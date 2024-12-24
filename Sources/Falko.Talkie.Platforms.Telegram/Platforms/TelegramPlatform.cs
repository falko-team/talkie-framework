using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Talkie.Bridges.Telegram.Clients;
using Talkie.Controllers;
using Talkie.Controllers.MessageControllers;
using Talkie.Flows;
using Talkie.Models.Identifiers;
using Talkie.Models.Profiles;

namespace Talkie.Platforms;

public sealed record TelegramPlatform : IPlatform, IDisposable
{
    public TelegramPlatform
    (
        ISignalFlow flow,
        ITelegramClient botApiClient,
        IBotProfile botProfile
    )
    {
        BotApiClient = botApiClient;
        BotProfile = botProfile;

        ControllerCreator = ControllerCreatorBuilder.Create()
            .AddMessageController(environmentProfileIdentifier => new TelegramMessageController(flow,
                this,
                environmentProfileIdentifier))
            .Build();
    }

    public IIdentifier Identifier => BotProfile.Identifier;

    [IgnoreDataMember]
    public ITelegramClient BotApiClient { get; }

    public IBotProfile BotProfile { get; }

    [IgnoreDataMember]
    public IControllerCreator ControllerCreator { get; }

    public void Dispose()
    {
        BotApiClient.Dispose();
    }
}
