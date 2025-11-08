namespace Telegram_clone.DTOs
{
    public record AuthResponseDto(
        int UserId,
        string Username,
        string Email,
        string Message
    );
}