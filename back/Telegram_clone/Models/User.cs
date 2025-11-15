namespace Telegram_clone.Models
{
    public partial class User
    {
            public int Id { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;

            
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? LastSeen { get; set; } 

            
            public ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
            public ICollection<Message> Messages { get; set; } = new List<Message>();
        }
    }

