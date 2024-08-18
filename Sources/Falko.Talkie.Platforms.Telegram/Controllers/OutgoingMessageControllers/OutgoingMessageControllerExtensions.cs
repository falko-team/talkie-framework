using System.Diagnostics.CodeAnalysis;

namespace Talkie.Controllers.OutgoingMessageControllers;

public static partial class OutgoingMessageControllerExtensions
{
    public static TelegramOutgoingMessageController? AsTelegram(this IOutgoingMessageController controller)
    {
        return controller as TelegramOutgoingMessageController;
    }

    public static bool TryGetTelegram(this IOutgoingMessageController controller, [NotNullWhen(true)] out TelegramOutgoingMessageController? telegramController)
    {
        telegramController = controller as TelegramOutgoingMessageController;
        return telegramController is not null;
    }

    public static TelegramOutgoingMessageController GetTelegram(this IOutgoingMessageController controller)
    {
        return controller as TelegramOutgoingMessageController
            ?? throw new InvalidOperationException("Controller is not a Telegram controller.");
    }

    public static bool IsTelegram(this IOutgoingMessageController controller)
    {
        return controller is TelegramOutgoingMessageController;
    }
}
