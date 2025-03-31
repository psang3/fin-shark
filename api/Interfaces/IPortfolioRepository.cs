using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetUserPortfolioAsync(string userId);
        Task<Portfolio?> GetByIdAsync(int id);
        Task<Portfolio?> GetUserStockAsync(string userId, string stockId);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> UpdateAsync(Portfolio portfolio);
        Task DeleteAsync(Portfolio portfolio);
    }
}