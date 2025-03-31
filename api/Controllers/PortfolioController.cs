using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Portfolio;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    [Authorize]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;

        public PortfolioController(
            IPortfolioRepository portfolioRepo,
            IStockRepository stockRepo,
            UserManager<AppUser> userManager)
        {
            _portfolioRepo = portfolioRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var portfolioItems = await _portfolioRepo.GetUserPortfolioAsync(user.Id);
            var portfolioDtos = portfolioItems.Select(p => new PortfolioDto
            {
                Id = p.Id,
                Symbol = p.Stock.Symbol,
                CompanyName = p.Stock.CompanyName,
                Quantity = p.Quantity
            });

            return Ok(portfolioDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddToPortfolio([FromBody] CreatePortfolioDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Find the stock
            var stock = await _stockRepo.GetBySymbolAsync(createDto.Symbol);
            if (stock == null)
                return NotFound($"Stock with symbol {createDto.Symbol} not found");

            // Check if user already has this stock in portfolio
            var existingPortfolio = await _portfolioRepo.GetUserStockAsync(user.Id, stock.Id);
            if (existingPortfolio != null)
                return BadRequest($"Stock {createDto.Symbol} is already in your portfolio");

            // Create new portfolio entry
            var portfolioItem = new Portfolio
            {
                AppUserId = user.Id,
                StockId = stock.Id,
                Quantity = createDto.Quantity
            };

            var created = await _portfolioRepo.CreateAsync(portfolioItem);
            
            return CreatedAtAction(nameof(GetUserPortfolio), new PortfolioDto
            {
                Id = created.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Quantity = created.Quantity
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortfolio(int id, [FromBody] UpdatePortfolioDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var portfolio = await _portfolioRepo.GetByIdAsync(id);
            if (portfolio == null)
                return NotFound($"Portfolio with id {id} not found");

            if (portfolio.AppUserId != user.Id)
                return Unauthorized();

            portfolio.Quantity = updateDto.Quantity;
            var updated = await _portfolioRepo.UpdateAsync(portfolio);

            return Ok(new PortfolioDto
            {
                Id = updated.Id,
                Symbol = updated.Stock.Symbol,
                CompanyName = updated.Stock.CompanyName,
                Quantity = updated.Quantity
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromPortfolio(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var portfolio = await _portfolioRepo.GetByIdAsync(id);
            if (portfolio == null)
                return NotFound($"Portfolio with id {id} not found");

            if (portfolio.AppUserId != user.Id)
                return Unauthorized();

            await _portfolioRepo.DeleteAsync(portfolio);
            return NoContent();
        }
    }
}