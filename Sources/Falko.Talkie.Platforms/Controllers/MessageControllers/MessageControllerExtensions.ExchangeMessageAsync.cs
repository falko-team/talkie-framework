using Talkie.Models;
using Talkie.Models.Identifiers;
using Talkie.Models.Messages;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;

namespace Talkie.Controllers.MessageControllers;

public static partial class MessageControllerExtensions
{
    public static Task<IIncomingMessage> ExchangeMessageAsync(this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        MessageContent content,
        CancellationToken cancellationToken = default)
    {
        return controller.ExchangeMessageAsync(messageIdentifier,
            new OutgoingMessage { Content = content },
            cancellationToken);
    }

    public static Task<IIncomingMessage> ExchangeMessageAsync(this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        IOutgoingMessageMutator messageMutator,
        CancellationToken cancellationToken = default)
    {
        return controller.ExchangeMessageAsync(messageIdentifier,
            messageMutator.Mutate(),
            cancellationToken);
    }

    public static Task<IIncomingMessage> ExchangeMessageAsync(this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        IOutgoingMessageBuilder messageBuilder,
        CancellationToken cancellationToken = default)
    {
        return controller.ExchangeMessageAsync(messageIdentifier,
            messageBuilder.Build(),
            cancellationToken);
    }

    public static Task<IIncomingMessage> ExchangeMessageAsync(this IMessageController controller,
        GlobalMessageIdentifier messageIdentifier,
        Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> messageBuilderFactory,
        CancellationToken cancellationToken = default)
    {
        return controller.ExchangeMessageAsync(messageIdentifier,
            messageBuilderFactory(new OutgoingMessageBuilder()).Build(),
            cancellationToken);
    }
}
