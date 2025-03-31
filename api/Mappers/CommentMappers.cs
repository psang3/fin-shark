using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                AppUserId = comment.AppUserId
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, string stockId, string userId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId,
                AppUserId = userId
            };
        }
    }
} 