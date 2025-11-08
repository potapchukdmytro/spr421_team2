

namespace Telegram_clone.DTOs
{
    public record CreateChatDto(
        string Name,
        bool IsGroup,
        List<int> MemberIds
    );
}