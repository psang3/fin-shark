using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetAllAsync(query);

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockDto.ToStockFromCreateDto();

            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = stockModel.Id }, 
                stockModel.ToStockDto()
            );
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.GetByIdAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            var updatedStock = await _stockRepo.UpdateAsync(stockModel);
            if (updatedStock == null)
            {
                return NotFound();
            }

            return Ok(updatedStock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}