namespace Telegram_clone.Models
{
    public partial class User
    {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }

            //хезе крч, штукі по тіпу ДейтТаймів двох останніх чі нада, хай пока будуть, но есчо вони опціональні
            public DateTime CreatedAt { get; set; }
            public DateTime? LastSeen { get; set; } 

            
            public ICollection<ChatMember> ChatMembers { get; set; }
            public ICollection<Message> Messages { get; set; }
        }
    }

