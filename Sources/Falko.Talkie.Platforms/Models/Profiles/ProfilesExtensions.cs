namespace Talkie.Models.Profiles;

public static partial class ProfilesExtensions
{
    public static IUserProfile? AsUser(this IProfile profile)
    {
        return profile as IUserProfile;
    }

    public static IChatProfile? AsChat(this IProfile profile)
    {
        return profile as IChatProfile;
    }

    public static IBotProfile? AsBot(this IProfile profile)
    {
        return profile as IBotProfile;
    }
}
