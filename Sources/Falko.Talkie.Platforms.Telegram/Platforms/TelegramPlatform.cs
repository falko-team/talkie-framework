using Falko.Talkie.Bridges.Telegram.Clients;
using Falko.Talkie.Controllers;
using Falko.Talkie.Models.Messages;
using Falko.Talkie.Models.Profiles;

namespace Falko.Talkie.Platforms;

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
