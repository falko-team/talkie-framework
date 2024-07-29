namespace Talkie.Models.Profiles;

public interface IProfile
{
    Identifier Identifier { get; }

    string? Language { get; }
}
