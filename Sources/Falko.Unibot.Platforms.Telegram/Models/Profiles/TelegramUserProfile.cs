namespace Falko.Unibot.Models.Profiles;

public sealed record TelegramUserProfile : IUserProfile
{
    public required Identifier Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? NickName { get; init; }

    public string? Description { get; init; }
}
