using api.Models;
using api.Helpers;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
} 