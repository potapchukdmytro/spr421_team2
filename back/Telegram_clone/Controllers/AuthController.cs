using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram_clone.Data;
using Telegram_clone.DTOs;
using Telegram_clone.Models;

namespace Telegram_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        //register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
        {
            
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("All fields are required");
            }

            
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Email is already taken");
            }

            
            if (await _context.Users.AnyAsync(u => u.UserName == dto.Username))
            {
                return BadRequest("Username is already taken");
            }

            
            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password, 
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto(
                user.Id,
                user.UserName,
                user.Email,
                "Registration successful"
            ));
        }

        // login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("All fields are required");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            
            if (user.PasswordHash != dto.Password)
            {
                return Unauthorized("Invalid email or password");
            }

           
            user.LastSeen = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto(
                user.Id,
                user.UserName,
                user.Email,
                "Login successful"
            ));
        }

        // get all users
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.CreatedAt,
                    u.LastSeen
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
