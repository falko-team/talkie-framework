namespace Talkie.Models.Identifiers;

public sealed record TelegramMessageIdentifier
(
    long MessageIdentifier,
    string? ConnectionIdentifier = null
) : IMessageIdentifier
{
    public static implicit operator TelegramMessageIdentifier(long messageIdentifier) => new(messageIdentifier);
}
