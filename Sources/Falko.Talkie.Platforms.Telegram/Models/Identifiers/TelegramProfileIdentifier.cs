namespace Falko.Talkie.Models.Identifiers;

public sealed record TelegramProfileIdentifier(long ProfileIdentifier) : IProfileIdentifier
{
    public static implicit operator TelegramProfileIdentifier(long profileIdentifier) => new(profileIdentifier);
}
