namespace Falko.Talkie.Models.Identifiers;

public sealed record TelegramMessageFileAttachmentGroupIdentifier(string? GroupIdentifier = null) : IMessageAttachmentIdentifier
{
    public static readonly TelegramMessageFileAttachmentGroupIdentifier Empty = new();

    public static implicit operator TelegramMessageFileAttachmentGroupIdentifier(string groupIdentifier) => new(groupIdentifier);
}
