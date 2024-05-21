namespace Talkie.Models.Profiles;

public interface IUserProfile : IProfile
{
    string? FirstName { get; }

    string? LastName { get; }

    string? NickName { get; }

    string? Description { get; }
}
