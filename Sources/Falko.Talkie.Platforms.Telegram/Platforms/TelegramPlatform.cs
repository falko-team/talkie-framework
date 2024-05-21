using Talkie.Bridges.Telegram.Clients;
using Talkie.Controllers;
using Talkie.Models.Messages;
using Talkie.Models.Profiles;

namespace Talkie.Platforms;

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
