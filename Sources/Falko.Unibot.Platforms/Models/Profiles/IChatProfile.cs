namespace Falko.Unibot.Models.Profiles;

public interface IChatProfile : IProfile
{
    string? Title { get; }

    string? Description { get; }
}
