using System.Runtime.Serialization;
using Talkie.Bridges.Telegram.Clients;
using Talkie.Bridges.Telegram.Policies;
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
        IBotProfile profile,
        TimeSpan defaultRetryDelay
    )
    {
        Client = client;
        Profile = profile;

        _messageControllerFactory = new TelegramMessageControllerFactory(flow, this);

        Policy = new TelegramTooManyRequestGlobalRetryPolicy(defaultRetryDelay);
    }

    public IIdentifier Identifier => Profile.Identifier;

    public IBotProfile Profile { get; }

    [IgnoreDataMember]
    internal ITelegramClient Client { get; }

    [IgnoreDataMember]
    internal ITelegramRetryPolicy Policy { get; }

    public void Dispose()
    {
        Client.Dispose();
    }

    [IgnoreDataMember]
    IControllerFactory<IMessageController, GlobalMessageIdentifier> IWithControllerFactory<IMessageController, GlobalMessageIdentifier>.Factory
        => _messageControllerFactory;

    [IgnoreDataMember]
    IControllerFactory<IMessageAttachmentController, Unit> IWithControllerFactory<IMessageAttachmentController, Unit>.Factory
        => TelegramMessageAttachmentControllerFactory.Instance;
}
