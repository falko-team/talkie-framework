using Talkie.Bridges.Telegram.Models;
using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Connections;

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
        IProfile receiverProfile,
        DateTime? publishedDate,
        string? connectionIdentifier = null,
        TelegramMessage? reply = null
    )
    {
        var receivedDate = DateTime.UtcNow;

        MessageIdentifier = messageIdentifier;
        Platform = platform;
        EnvironmentProfile = environmentProfile;
        PublisherProfile = publisherProfile;
        ReceiverProfile = receiverProfile;
        ConnectionIdentifier = connectionIdentifier;
        Reply = reply;
        PublishedDate = publishedDate ?? receivedDate;
        ReceivedDate = receivedDate;
    }
}
