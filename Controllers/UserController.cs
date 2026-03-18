using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheITBookOnlineShop.Data;
using TheITBookOnlineShop.Models.DTOs;
using TheITBookOnlineShop.Models.Entities;

namespace TheITBookOnlineShop.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(AppDbContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("like")]
        public async Task<IActionResult> LikeBook([FromBody] LikeBookRequest request)
        {
            try
            {
                
                var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
                if (!userExists)
                {
                    return NotFound(new { message = "User not found." });
                }

                
                var alreadyLiked = await _context.UserLikes
                    .AnyAsync(ul => ul.UserId == request.UserId && ul.BookId == request.BookId);

                if (alreadyLiked)
                {
                    return BadRequest(new { message = "User has already liked this book." });
                }

               
                var newLike = new UserLike
                {
                    UserId = request.UserId,
                    BookId = request.BookId,
                    LikedAt = DateTime.UtcNow
                };

                _context.UserLikes.Add(newLike);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Book liked successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while liking the book for UserID {UserId}", request.UserId);
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }
    }
}