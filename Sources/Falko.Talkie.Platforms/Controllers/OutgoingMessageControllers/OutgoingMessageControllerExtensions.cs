using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Outgoing;

namespace Talkie.Controllers.OutgoingMessageControllers;

public static partial class OutgoingMessageControllerExtensions
{
    public static Task SendMessageAsync(this IOutgoingMessageController controller, IOutgoingMessageMutator mutator,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.SendMessageAsync(mutator.Mutate(), features, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, IOutgoingMessageMutator mutator,
        CancellationToken cancellationToken)
    {
        return controller.SendMessageAsync(mutator, default, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, IOutgoingMessageBuilder builder,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.SendMessageAsync(builder.Build(), features, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, IOutgoingMessageBuilder builder,
        CancellationToken cancellationToken)
    {
        return controller.SendMessageAsync(builder, default, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builderFactory,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.SendMessageAsync(builderFactory(new OutgoingMessageBuilder()).Build(), features, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builderFactory,
        CancellationToken cancellationToken)
    {
        return controller.SendMessageAsync(builderFactory, default, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, MessageContent content,
        MessagePublishingFeatures features = default,
        CancellationToken cancellationToken = default)
    {
        return controller.SendMessageAsync(new OutgoingMessage { Content = content }, features, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, MessageContent content,
        CancellationToken cancellationToken)
    {
        return controller.SendMessageAsync(content, default, cancellationToken);
    }

    public static Task SendMessageAsync(this IOutgoingMessageController controller, IOutgoingMessage message,
        CancellationToken cancellationToken)
    {
        return controller.SendMessageAsync(message, default, cancellationToken);
    }
}
