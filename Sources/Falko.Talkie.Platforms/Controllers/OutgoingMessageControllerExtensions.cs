using Talkie.Builders;

namespace Talkie.Controllers;

public static partial class OutgoingMessageControllerExtensions
{
    public static Task PublishMessageAsync(this IOutgoingMessageController controller, Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builder,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(builder(new OutgoingMessageBuilder()).Build(), cancellationToken);
    }

    public static Task PublishMessageAsync(this IOutgoingMessageController controller, string text,
        CancellationToken cancellationToken = default)
    {
        return controller.PublishMessageAsync(message => message.AddText(text), cancellationToken);
    }
}
