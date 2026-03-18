using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheITBookOnlineShop.Data;
using TheITBookOnlineShop.Models.DTOs;
using TheITBookOnlineShop.Models.Entities;

namespace TheITBookOnlineShop.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

                if (existingUser != null)
                {
                    return BadRequest(new { message = "Username is already taken." });
                }

                
                var newUser = new User
                {
                    Username = request.Username,
                    Password = request.Password, 
                    FullName = request.Fullname
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration.");
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or password." });
                }

                
                return Ok(new
                {
                    message = "Login successful.",
                    userId = user.Id,
                    fullname = user.FullName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }
    }
}