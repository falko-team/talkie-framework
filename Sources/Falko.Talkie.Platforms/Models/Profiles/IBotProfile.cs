namespace Falko.Talkie.Models.Profiles;

public interface IBotProfile : IProfile
{
    string? FirstName { get; }

    string? LastName { get; }

    string? NickName { get; }

    string? Description { get; }
}
