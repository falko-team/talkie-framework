using Falko.Talkie.Models.Entries;
using Falko.Talkie.Platforms;

namespace Falko.Talkie.Models.Messages;

public static class MessageExtensions
{
    public static bool TryGetIdentifier(this IMessage message, out Identifier identifier)
    {
        if (message is Message.IWithIdentifier withIdentifier)
        {
            identifier = withIdentifier.Id;
            return true;
        }

        identifier = default;
        return false;
    }

    public static bool TryGetPlatform(this IMessage message, out IPlatform platform)
    {
        if (message is Message.IWithPlatform withPlatform)
        {
            platform = withPlatform.Platform;
            return true;
        }

        platform = default;
        return false;
    }

    public static bool TryGetEntry(this IMessage message, out IEntry entry)
    {
        if (message is Message.IWithEntry withEntry)
        {
            entry = withEntry.Entry;
            return true;
        }

        entry = default;
        return false;
    }
}
