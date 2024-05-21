namespace Falko.Talkie.Models.Profiles;

public sealed record TelegramChatProfile : IChatProfile
{
    public required Identifier Id { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }
}
