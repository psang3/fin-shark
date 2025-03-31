using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string StockId { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;

        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
        
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
    }
} 