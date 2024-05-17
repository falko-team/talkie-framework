namespace Falko.Unibot.Models.Profiles;

public sealed record TelegramBotProfile : IBotProfile
{
    public required Identifier Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? NickName { get; init; }

    public string? Description { get; init; }
}
