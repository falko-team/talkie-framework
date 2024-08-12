using Talkie.Globalization;

namespace Talkie.Models.Profiles;

public sealed record TelegramBotProfile : IBotProfile
{
    public required Identifier Identifier { get; init; }

    public Language Language { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? NickName { get; init; }

    public string? Description { get; init; }
}
