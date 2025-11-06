namespace Telegram_clone.Hubs
{
    public partial class ChatHub
    {
        public record UserConnection(string UserName, string ChatRoom);
    }
}
