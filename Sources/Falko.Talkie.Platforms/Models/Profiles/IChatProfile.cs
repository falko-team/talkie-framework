namespace Talkie.Models.Profiles;

public interface IChatProfile : IProfile
{
    string? Title { get; }

    string? NickName { get; }

    string? Description { get; }
}
