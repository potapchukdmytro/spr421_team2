using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram_clone.Data;
using Telegram_clone.DTOs;
using Telegram_clone.Models;

namespace Telegram_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        // get all user chats
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<ChatDto>>> GetUserChats(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return NotFound("User not found");
            }

            var chats = await _context.ChatMembers
                .Where(cm => cm.UserId == userId)
                .Include(cm => cm.Chat)
                    .ThenInclude(c => c.Members)
                    .ThenInclude(m => m.User)
                .Select(cm => new ChatDto(
                    cm.Chat.Id,
                    cm.Chat.Name,
                    cm.Chat.IsGroup,
                    cm.Chat.Members.Select(m => m.User.UserName).ToList(),
                    cm.Chat.CreatedAt
                ))
                .ToListAsync();

            return Ok(chats);
        }

        // create
        [HttpPost("create")]
        public async Task<ActionResult> CreateChat(CreateChatDto dto)
        {
            
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("The chat name is required");
            }

            if (dto.MemberIds == null || dto.MemberIds.Count < 2)
            {
                return BadRequest("At least two members are required");
            }

            
            var users = await _context.Users
                .Where(u => dto.MemberIds.Contains(u.Id))
                .ToListAsync();

            if (users.Count != dto.MemberIds.Count)
            {
                return BadRequest("Some users were not found");
            }

           
            var chat = new Chat
            {
                Name = dto.Name,
                IsGroup = dto.IsGroup,
                CreatedAt = DateTime.UtcNow
            };

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

           
            foreach (var userId in dto.MemberIds)
            {
                var chatMember = new ChatMember
                {
                    ChatId = chat.Id,
                    UserId = userId,
                    JoinedAt = DateTime.UtcNow
                };
                _context.ChatMembers.Add(chatMember);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                chatId = chat.Id,
                message = "Chat created successfully"
            });
        }

        // get chat by id
        [HttpGet("{chatId}")]
        public async Task<ActionResult<ChatDto>> GetChat(int chatId)
        {
            var chat = await _context.Chats
                .Include(c => c.Members)
                    .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(c => c.Id == chatId);

            if (chat == null)
            {
                return NotFound("Chat not found");
            }

            var chatDto = new ChatDto(
                chat.Id,
                chat.Name,
                chat.IsGroup,
                chat.Members.Select(m => m.User.UserName).ToList(),
                chat.CreatedAt
            );

            return Ok(chatDto);
        }

        // delete chat by id
        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            var chat = await _context.Chats.FindAsync(chatId);

            if (chat == null)
            {
                return NotFound("Chat not found");
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Chat deleted successfully" });
        }
    }
}