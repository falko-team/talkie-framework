using System.Runtime.Serialization;
using Talkie.Bridges.Telegram.Clients;
using Talkie.Common;
using Talkie.Controllers;
using Talkie.Controllers.AttachmentControllers;
using Talkie.Controllers.MessageControllers;
using Talkie.Flows;
using Talkie.Models.Identifiers;
using Talkie.Models.Profiles;

namespace Talkie.Platforms;

public sealed record TelegramPlatform : IPlatform, IWithMessageControllerFactory, IWithAttachmentControllerFactory, IDisposable
{
    private readonly TelegramMessageControllerFactory _messageControllerFactory;

    public TelegramPlatform
    (
        ISignalFlow flow,
        ITelegramClient client,
        IBotProfile profile
    )
    {
        Client = client;
        Profile = profile;

        _messageControllerFactory = new TelegramMessageControllerFactory(flow, this);
    }

    public IIdentifier Identifier => Profile.Identifier;

    public IBotProfile Profile { get; }

    [IgnoreDataMember]
    internal ITelegramClient Client { get; }

    public void Dispose()
    {
        Client.Dispose();
    }

    [IgnoreDataMember]
    IControllerFactory<IMessageController, GlobalMessageIdentifier> IWithControllerFactory<IMessageController, GlobalMessageIdentifier>.Factory
        => _messageControllerFactory;

    [IgnoreDataMember]
    IControllerFactory<IAttachmentController, Nothing> IWithControllerFactory<IAttachmentController, Nothing>.Factory
        => TelegramAttachmentControllerFactory.Instance;
}
