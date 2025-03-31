using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Portfolio
{
    public class PortfolioDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string CompanyName { get; set; } = string.Empty;
    }

    public class CreatePortfolioDto
    {
        [Required]
        public string Symbol { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }
    }

    public class UpdatePortfolioDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }
    }
} 