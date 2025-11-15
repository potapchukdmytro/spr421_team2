using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram_clone.Data;
using Telegram_clone.DTOs;

namespace Telegram_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessagesController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<List<MessageDto>>> GetChatMessages(
            int chatId,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            var chatExists = await _context.Chats.AnyAsync(c => c.Id == chatId);
            if (!chatExists)
            {
                return NotFound("Чат не знайдено");
            }

            var messages = await _context.Messages
                .Where(m => m.ChatId == chatId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.SentAt) 
                .Skip(skip)
                .Take(take)
                .Select(m => new MessageDto(
                    m.Id,
                    m.Content,
                    m.SentAt,
                    m.SenderId,
                    m.Sender.UserName,
                    m.ChatId,
                    m.IsRead
                ))
                .ToListAsync();

            return Ok(messages.OrderBy(m => m.SentAt)); 
        }

        
        [HttpGet("{messageId}")]
        public async Task<ActionResult<MessageDto>> GetMessage(int messageId)
        {
            var message = await _context.Messages
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == messageId);

            if (message == null)
            {
                return NotFound("Повідомлення не знайдено");
            }

            var messageDto = new MessageDto(
                message.Id,
                message.Content,
                message.SentAt,
                message.SenderId,
                message.Sender.UserName,
                message.ChatId,
                message.IsRead
            );

            return Ok(messageDto);
        }

        

        
      
    }
}