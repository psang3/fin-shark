using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }
        
        public string AppUserId { get; set; } = string.Empty;
        public string StockId { get; set; } = string.Empty;
        public decimal Quantity { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; } = null!;

        [ForeignKey("StockId")]
        public Stock Stock { get; set; } = null!;
    }
}
