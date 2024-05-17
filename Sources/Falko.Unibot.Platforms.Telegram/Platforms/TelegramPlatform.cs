using Falko.Unibot.Bridges.Telegram.Clients;
using Falko.Unibot.Controllers;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;

namespace Falko.Unibot.Platforms;

public sealed class TelegramPlatform : IPlatform
{
    public TelegramPlatform(ITelegramBotApiClient client, IBotProfile self)
    {
        Self = self;

        ControllerCreator = ControllerCreatorBuilder.Create()
            .Add<IOutgoingMessageController, IMessage>(message => new TelegramOutgoingMessageController(client, message))
            .Build();
    }

    public IBotProfile Self { get; }

    public IControllerCreator ControllerCreator { get; }
}
