namespace Telegram_clone.Models
{
    public partial class ChatMember
    {
        public int Id { get; set; }
        public DateTime JoinedAt { get; set; }

        
        public int ChatId { get; set; }
        public int UserId { get; set; }

        
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}
