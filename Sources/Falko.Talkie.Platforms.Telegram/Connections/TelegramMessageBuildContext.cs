using Falko.Talkie.Bridges.Telegram.Models;
using Falko.Talkie.Models.Profiles;
using Falko.Talkie.Platforms;

namespace Falko.Talkie.Connections;

internal readonly ref struct TelegramMessageBuildContext
{
    public readonly long MessageIdentifier;

    public readonly string? ConnectionIdentifier;

    public readonly TelegramPlatform Platform;

    public readonly TelegramMessage? Reply;

    public readonly IProfile EnvironmentProfile;

    public readonly IProfile PublisherProfile;

    public readonly IProfile ReceiverProfile;

    public readonly DateTime PublishedDate;

    public readonly DateTime ReceivedDate;

    public TelegramMessageBuildContext
    (
        long messageIdentifier,
        TelegramPlatform platform,
        IProfile environmentProfile,
        IProfile publisherProfile,
        DateTime? publishedDate,
        string? connectionIdentifier = null,
        TelegramMessage? reply = null
    )
    {
        var receivedDate = DateTime.UtcNow;

        MessageIdentifier = messageIdentifier;
        Platform = platform;
        EnvironmentProfile = environmentProfile;
        PublisherProfile = IsBusinessSelfSent(environmentProfile, publisherProfile, connectionIdentifier)
            ? platform.Profile
            : publisherProfile;
        ReceiverProfile = platform.Profile;
        ConnectionIdentifier = connectionIdentifier;
        Reply = reply;
        PublishedDate = publishedDate ?? receivedDate;
        ReceivedDate = receivedDate;
    }

    private static bool IsBusinessSelfSent(IProfile environmentProfile, IProfile publisherProfile, string? connectionIdentifier)
    {
        if (connectionIdentifier is null) return false;

        return Equals(environmentProfile.Identifier, publisherProfile.Identifier) is false;
    }
}
