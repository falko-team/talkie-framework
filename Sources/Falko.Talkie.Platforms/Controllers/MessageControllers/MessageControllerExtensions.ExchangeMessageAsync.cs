using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Models.Messages.Outgoing;

namespace Falko.Talkie.Controllers.MessageControllers;

public static partial class MessageControllerExtensions
{
    public static Task<IIncomingMessage> ExchangeMessageAsync
    (
        this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        MessageContent content,
        CancellationToken cancellationToken = default
    )
    {
        return controller.ExchangeMessageAsync
        (
            messageIdentifier,
            new OutgoingMessage { Content = content },
            cancellationToken
        );
    }

    public static Task<IIncomingMessage> ExchangeMessageAsync
    (
        this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        IOutgoingMessageMutator messageMutator,
        CancellationToken cancellationToken = default
    )
    {
        return controller.ExchangeMessageAsync(messageIdentifier, messageMutator.Mutate(), cancellationToken);
    }

    public static Task<IIncomingMessage> ExchangeMessageAsync
    (
        this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        IOutgoingMessageBuilder messageBuilder,
        CancellationToken cancellationToken = default
    )
    {
        return controller.ExchangeMessageAsync(messageIdentifier, messageBuilder.Build(), cancellationToken);
    }

    public static Task<IIncomingMessage> ExchangeMessageAsync
    (
        this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> messageBuilderFactory,
        CancellationToken cancellationToken = default
    )
    {
        return controller.ExchangeMessageAsync
        (
            messageIdentifier,
            messageBuilderFactory(new OutgoingMessageBuilder()).Build(),
            cancellationToken
        );
    }
}
