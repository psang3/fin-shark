using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Portfolio>> GetUserPortfolioAsync(string userId)
        {
            return await _context.Portfolios
                .Include(p => p.Stock)
                .Where(p => p.AppUserId == userId)
                .ToListAsync();
        }

        public async Task<Portfolio?> GetByIdAsync(int id)
        {
            return await _context.Portfolios
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Portfolio?> GetUserStockAsync(string userId, string stockId)
        {
            return await _context.Portfolios
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(p => p.AppUserId == userId && p.StockId == stockId);
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> UpdateAsync(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task DeleteAsync(Portfolio portfolio)
        {
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
        }
    }
}