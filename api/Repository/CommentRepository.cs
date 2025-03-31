using api.Data;
using api.Interfaces;
using api.Models;
using api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var comments = _context.Comments.Include(c => c.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(queryObject.StockId))
            {
                comments = comments.Where(c => c.StockId == queryObject.StockId);
            }

            if (!string.IsNullOrEmpty(queryObject.Symbol))
            {
                comments = comments.Where(c => c.Stock.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                comments = queryObject.SortBy.ToLower() switch
                {
                    "title" => queryObject.IsDecsending ? comments.OrderByDescending(c => c.Title) : comments.OrderBy(c => c.Title),
                    "createdon" => queryObject.IsDecsending ? comments.OrderByDescending(c => c.CreatedOn) : comments.OrderBy(c => c.CreatedOn),
                    _ => comments.OrderByDescending(c => c.CreatedOn)
                };
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await comments
                .Skip(skipNumber)
                .Take(queryObject.PageSize)
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
} 