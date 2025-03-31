using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string StockId { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
    }

    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Content must be at least 10 characters")]
        [MaxLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
        public string Content { get; set; } = string.Empty;
    }

    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Content must be at least 10 characters")]
        [MaxLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
        public string Content { get; set; } = string.Empty;
    }
} 