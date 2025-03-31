using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(string id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(Stock stock);
        Task<Stock?> DeleteAsync(string id);
        Task<bool> StockExists(string id);
    }
}