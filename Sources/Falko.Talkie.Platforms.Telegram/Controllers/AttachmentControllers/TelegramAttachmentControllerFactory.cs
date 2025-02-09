using Talkie.Common;

namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramAttachmentControllerFactory : IControllerFactory<IAttachmentController, Nothing>
{
    public static TelegramAttachmentControllerFactory Instance { get; } = new();

    private readonly TelegramAttachmentController _instance = new();

    private TelegramAttachmentControllerFactory() { }

    public IAttachmentController Create(Nothing nothing)
    {
        return _instance;
    }
}
