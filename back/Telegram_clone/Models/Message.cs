namespace Telegram_clone.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

       
        public int ChatId { get; set; }
        public int SenderId { get; set; }

       
        public Chat Chat { get; set; } = null!;
        public User Sender { get; set; } = null!;
    }
}
