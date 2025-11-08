namespace Telegram_clone.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

       
        public int ChatId { get; set; }
        public int SenderId { get; set; }

       
        public Chat Chat { get; set; }
        public User Sender { get; set; }
    }
}
