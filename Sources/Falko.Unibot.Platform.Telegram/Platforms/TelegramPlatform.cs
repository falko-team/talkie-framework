using Falko.Unibot.Controllers;
using Falko.Unibot.Models.Messages;
using Falko.Unibot.Models.Profiles;
using Telegram.Bot;

namespace Falko.Unibot.Platforms;

public sealed class TelegramPlatform : IPlatform
{
    public TelegramPlatform(ITelegramBotClient client, IBotProfile self)
    {
        Self = self;

        ControllerCreator = ControllerCreatorBuilder.Create()
            .Add<IOutgoingMessageController, IMessage>(message => new TelegramOutgoingMessageController(client, message))
            .Build();
    }

    public IBotProfile Self { get; }

    public IControllerCreator ControllerCreator { get; }
}
