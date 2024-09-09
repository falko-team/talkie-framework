using Talkie.Bridges.Telegram.Clients;
using Talkie.Controllers;
using Talkie.Controllers.MessageControllers;
using Talkie.Flows;
using Talkie.Models;
using Talkie.Models.Profiles;

namespace Talkie.Platforms;

public sealed record TelegramPlatform : IPlatform
{
    public TelegramPlatform(ISignalFlow flow, ITelegramBotApiClient botApiClient,
        TelegramBotProfile botProfile)
    {
        BotApiClient = botApiClient;
        BotProfile = botProfile;

        ControllerCreator = ControllerCreatorBuilder.Create()
            .AddMessageController(environmentProfileIdentifier => new TelegramMessageController(flow,
                this,
                environmentProfileIdentifier))
            .Build();
    }

    public Identifier Identifier => BotProfile.Identifier;

    public ITelegramBotApiClient BotApiClient { get; }

    public TelegramBotProfile BotProfile { get; }

    public IControllerCreator ControllerCreator { get; }
}
