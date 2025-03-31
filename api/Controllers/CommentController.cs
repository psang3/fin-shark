using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(
            ICommentRepository commentRepo,
            UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _userManager = userManager;
        }

        // ... existing code ...
    }
} 