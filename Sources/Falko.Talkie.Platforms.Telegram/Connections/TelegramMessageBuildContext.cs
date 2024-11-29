using Talkie.Bridges.Telegram.Models;
using Talkie.Models.Profiles;
using Talkie.Platforms;

namespace Talkie.Connections;

internal readonly ref struct TelegramMessageBuildContext
{
    public readonly long Identifier;

    public readonly TelegramPlatform Platform;

    public readonly TelegramMessage? Reply;

    public readonly IProfile EnvironmentProfile;

    public readonly IProfile PublisherProfile;

    public readonly DateTime PublishedDate;

    public readonly DateTime ReceivedDate;

    public TelegramMessageBuildContext
    (
        long identifier,
        TelegramPlatform platform,
        IProfile environmentProfile,
        IProfile publisherProfile,
        DateTime? publishedDate,
        TelegramMessage? reply = null)
    {
        var receivedDate = DateTime.UtcNow;

        Identifier = identifier;
        Platform = platform;
        EnvironmentProfile = environmentProfile;
        PublisherProfile = publisherProfile;
        Reply = reply;
        PublishedDate = publishedDate ?? receivedDate;
        ReceivedDate = receivedDate;
    }
}
