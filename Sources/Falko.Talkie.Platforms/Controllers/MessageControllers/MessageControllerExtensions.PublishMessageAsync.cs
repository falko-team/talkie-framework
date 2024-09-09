using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Incoming;
using Talkie.Models.Messages.Outgoing;

namespace Talkie.Controllers.MessageControllers;

public static partial class MessageControllerExtensions
{
    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        IOutgoingMessageMutator mutator,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(mutator.Mutate(), features, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        IOutgoingMessageMutator mutator,
        CancellationToken cancellationToken)
    {
        return controller.PublishMessageAsync(mutator, default, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        IOutgoingMessageBuilder builder,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(builder.Build(), features, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        IOutgoingMessageBuilder builder,
        CancellationToken cancellationToken)
    {
        return controller.PublishMessageAsync(builder, default, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builderFactory,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(builderFactory(new OutgoingMessageBuilder()).Build(), features, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builderFactory,
        CancellationToken cancellationToken)
    {
        return controller.PublishMessageAsync(builderFactory, default, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        MessageContent content,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(new OutgoingMessage { Content = content }, features, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        MessageContent content,
        CancellationToken cancellationToken)
    {
        return controller.PublishMessageAsync(content, default, cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync(this IMessageController controller,
        IOutgoingMessage message,
        CancellationToken cancellationToken)
    {
        return controller.PublishMessageAsync(message, default, cancellationToken);
    }
}
