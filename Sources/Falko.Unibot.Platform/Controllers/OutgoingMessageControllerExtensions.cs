using Falko.Unibot.Builders;

namespace Falko.Unibot.Controllers;

public static class OutgoingMessageControllerExtensions
{
    public static Task SendAsync(this IOutgoingMessageController controller, Func<IOutgoingMessageBuilder, IOutgoingMessageBuilder> builder,
        CancellationToken cancellationToken = default)
    {
        return controller.SendAsync(builder(new OutgoingMessageBuilder()).Build(), cancellationToken);
    }
}
