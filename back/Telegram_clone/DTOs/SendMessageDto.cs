namespace Telegram_clone.DTOs
{
    public record SendMessageDto(
        int ChatId,
        int SenderId,
        string Content
    );
}