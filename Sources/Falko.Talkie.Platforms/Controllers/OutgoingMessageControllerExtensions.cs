using Talkie.Models.Messages;

namespace Talkie.Controllers;

public static partial class OutgoingMessageControllerExtensions
{
    public static Task PublishMessageAsync(this IOutgoingMessageController controller, IOutgoingMessageMutator mutator,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(mutator.Mutate(), cancellationToken);
    }

    public static Task PublishMessageAsync(this IOutgoingMessageController controller, IOutgoingMessageBuilder builder,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(builder.Build(), cancellationToken);
    }

    public static Task PublishMessageAsync(this IOutgoingMessageController controller, Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builderFactory,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(builderFactory(new OutgoingMessageBuilder()).Build(), cancellationToken);
    }

    public static Task PublishMessageAsync(this IOutgoingMessageController controller, string text,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(message => message.AddText(text), cancellationToken);
    }
}
