namespace Talkie.Models.Profiles;

public static partial class ProfilesExtensions
{
    public static IUserProfile? AsUserProfile(this IProfile profile)
    {
        return profile as IUserProfile;
    }

    public static IChatProfile? AsChatProfile(this IProfile profile)
    {
        return profile as IChatProfile;
    }

    public static IBotProfile? AsBotProfile(this IProfile profile)
    {
        return profile as IBotProfile;
    }
}
