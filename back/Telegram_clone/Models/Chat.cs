namespace Telegram_clone.Models
{
    public partial class Chat
    {
        public int Id { get; set; }
        public string ChatName { get; set; }
        public bool IsGroup { get; set; }
        public DateTime CreatedAt { get; set; }

        
        public ICollection<ChatMember> Members { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
