using Talkie.Bridges.Telegram.Clients;
using Talkie.Controllers;
using Talkie.Models;
using Talkie.Models.Messages;
using Talkie.Models.Profiles;

namespace Talkie.Platforms;

public sealed record TelegramPlatform : IPlatform
{
    public TelegramPlatform(ITelegramBotApiClient botApiClient,
        TelegramBotProfile botProfile)
    {
        BotApiClient = botApiClient;
        BotProfile = botProfile;

        ControllerCreator = ControllerCreatorBuilder.Create()
            .Add<IOutgoingMessageController, IIncomingMessage>(message => new TelegramOutgoingMessageController(this, message))
            .Build();
    }

    public Identifier Identifier => BotProfile.Identifier;

    public ITelegramBotApiClient BotApiClient { get; }

    public TelegramBotProfile BotProfile { get; }

    public IControllerCreator ControllerCreator { get; }
}
