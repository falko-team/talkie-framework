using System.Runtime.Serialization;
using Falko.Talkie.Bridges.Telegram.Clients;
using Falko.Talkie.Bridges.Telegram.Policies;
using Falko.Talkie.Common;
using Falko.Talkie.Controllers;
using Falko.Talkie.Controllers.AttachmentControllers;
using Falko.Talkie.Controllers.CommandControllers;
using Falko.Talkie.Controllers.CommandsControllers;
using Falko.Talkie.Controllers.MessageControllers;
using Falko.Talkie.Flows;
using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Profiles;

namespace Falko.Talkie.Platforms;

public sealed record TelegramPlatform : IPlatform, IDisposable,
    IWithMessageControllerFactory,
    IWithAttachmentControllerFactory,
    IWithCommandMessageControllerFactory
{
    private readonly TelegramMessageControllerFactory _messageControllerFactory;

    private readonly TelegramCommandControllerFactory _commandControllerFactory;

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
        _commandControllerFactory = new TelegramCommandControllerFactory(profile);

        Policy = new TelegramTooManyRequestGlobalRetryPolicy(defaultRetryDelay);
    }

    public IIdentifier Identifier => Profile.Identifier;

    public IBotProfile Profile { get; }

    [IgnoreDataMember]
    internal ITelegramClient Client { get; }

    [IgnoreDataMember]
    internal ITelegramRetryPolicy Policy { get; }

    public void Dispose() => Client.Dispose();

    [IgnoreDataMember]
    IControllerFactory<IMessageController, GlobalMessageIdentifier> IWithControllerFactory<IMessageController, GlobalMessageIdentifier>.Factory
        => _messageControllerFactory;

    [IgnoreDataMember]
    IControllerFactory<IMessageAttachmentController, Unit> IWithControllerFactory<IMessageAttachmentController, Unit>.Factory
        => TelegramMessageAttachmentControllerFactory.Instance;

    [IgnoreDataMember]
    IControllerFactory<ICommandController, string> IWithControllerFactory<ICommandController, string>.Factory
        => _commandControllerFactory;
}
