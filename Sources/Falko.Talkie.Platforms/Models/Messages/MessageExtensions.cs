using System.Diagnostics.CodeAnalysis;
using Talkie.Models.Entries;
using Talkie.Platforms;

namespace Talkie.Models.Messages;

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

    public static bool TryGetPlatform(this IMessage message, [MaybeNullWhen(false)] out IPlatform platform)
    {
        if (message is Message.IWithPlatform withPlatform)
        {
            platform = withPlatform.Platform;
            return true;
        }

        platform = default;
        return false;
    }

    public static bool TryGetEntry(this IMessage message, [MaybeNullWhen(false)] out IEntry entry)
    {
        if (message is Message.IWithEntry withEntry)
        {
            entry = withEntry.Entry;
            return true;
        }

        entry = default;
        return false;
    }

    public static bool TryGetReply(this IMessage message, [MaybeNullWhen(false)] out IMessage replyMessage)
    {
        if (message is Message.IWithReply withReply)
        {
            replyMessage = withReply.Reply;
            return replyMessage is not null;
        }

        replyMessage = default;
        return false;
    }
}
