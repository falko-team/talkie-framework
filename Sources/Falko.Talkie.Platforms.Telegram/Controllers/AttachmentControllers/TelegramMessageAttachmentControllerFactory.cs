using Talkie.Common;

namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramMessageAttachmentControllerFactory : IControllerFactory<IMessageAttachmentController, Unit>
{
    public static TelegramMessageAttachmentControllerFactory Instance { get; } = new();

    private readonly TelegramMessageAttachmentController _instance = new();

    private TelegramMessageAttachmentControllerFactory() { }

    public IMessageAttachmentController Create(Unit unit)
    {
        return _instance;
    }
}
