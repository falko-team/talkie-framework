using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Incoming;
using Falko.Talkie.Models.Messages.Outgoing;

namespace Falko.Talkie.Controllers.MessageControllers;

public static partial class MessageControllerExtensions
{
    public static Task<IIncomingMessage> PublishMessageAsync
    (
        this IMessageController controller,
        IOutgoingMessageMutator mutator,
        CancellationToken cancellationToken = default
    )
    {
        return controller.PublishMessageAsync(mutator.Mutate(), cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync
    (
        this IMessageController controller,
        IOutgoingMessageBuilder builder,
        CancellationToken cancellationToken = default
    )
    {
        return controller.PublishMessageAsync(builder.Build(), cancellationToken);
    }

    public static Task<IIncomingMessage> PublishMessageAsync
    (
        this IMessageController controller,
        Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builderFactory,
        CancellationToken cancellationToken = default
    )
    {
        return controller.PublishMessageAsync
        (
            builderFactory(new OutgoingMessageBuilder()).Build(),
            cancellationToken
        );
    }

    public static Task<IIncomingMessage> PublishMessageAsync
    (
        this IMessageController controller,
        MessageContent content,
        CancellationToken cancellationToken = default
    )
    {
        return controller.PublishMessageAsync
        (
            new OutgoingMessage { Content = content },
            cancellationToken
        );
    }
}
