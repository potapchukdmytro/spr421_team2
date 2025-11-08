namespace Telegram_clone.DTOs
{
    public record MessageDto(
        int Id,
        string Content,
        DateTime SentAt,
        int SenderId,
        string SenderUsername,
        int ChatId,
        bool IsRead
    );
}