using Falko.Unibot.Bridges.Telegram.Clients;
using Falko.Unibot.Controllers;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;

namespace Falko.Unibot.Platforms;

public sealed record TelegramPlatform : IPlatform
{
    public TelegramPlatform(ITelegramBotApiClient client, IBotProfile self)
    {
        Client = client;
        Self = self;

        ControllerCreator = ControllerCreatorBuilder.Create()
            .Add<IOutgoingMessageController, IIncomingMessage>(message => new TelegramOutgoingMessageController(this, message))
            .Build();
    }

    public ITelegramBotApiClient Client { get; }

    public IBotProfile Self { get; }

    public IControllerCreator ControllerCreator { get; }
}
