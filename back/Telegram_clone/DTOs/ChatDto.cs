using System;


namespace Telegram_clone.DTOs
{
    public record ChatDto(
        int Id,
        string Name,
        bool IsGroup,
        List<string> MemberUsernames,
        DateTime CreatedAt
    );
}