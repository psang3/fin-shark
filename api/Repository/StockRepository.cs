using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(string id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).ThenInclude(c => c.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(queryObject.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrEmpty(queryObject.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                stocks = queryObject.SortBy.ToLower() switch
                {
                    "symbol" => queryObject.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol),
                    "companyname" => queryObject.IsDecsending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName),
                    "marketcap" => queryObject.IsDecsending ? stocks.OrderByDescending(s => s.MarketCap) : stocks.OrderBy(s => s.MarketCap),
                    _ => stocks.OrderByDescending(s => s.MarketCap)
                };
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stocks
                .Skip(skipNumber)
                .Take(queryObject.PageSize)
                .ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(string id)
        {
            return await _context.Stocks
                .Include(s => s.Comments)
                .ThenInclude(c => c.AppUser)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks
                .Include(s => s.Comments)
                .ThenInclude(c => c.AppUser)
                .FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
        }

        public Task<bool> StockExists(string id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}