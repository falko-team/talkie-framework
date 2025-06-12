using System.Diagnostics.CodeAnalysis;

namespace Falko.Talkie.Controllers.MessageControllers;

public static partial class TelegramMessageControllerExtensions
{
    public static TelegramMessageController? AsTelegram(this IMessageController controller)
    {
        return controller as TelegramMessageController;
    }

    public static bool TryGetTelegram(this IMessageController controller, [NotNullWhen(true)] out TelegramMessageController? telegramController)
    {
        telegramController = controller as TelegramMessageController;
        return telegramController is not null;
    }

    public static TelegramMessageController GetTelegram(this IMessageController controller)
    {
        return controller as TelegramMessageController
            ?? throw new InvalidOperationException("Controller is not a Telegram controller.");
    }

    public static bool IsTelegram(this IMessageController controller)
    {
        return controller is TelegramMessageController;
    }
}
