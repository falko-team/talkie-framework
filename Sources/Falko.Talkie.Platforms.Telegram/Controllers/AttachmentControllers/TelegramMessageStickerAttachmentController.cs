using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramMessageStickerAttachmentController : IMessageSickerAttachmentController
{
    public static readonly TelegramMessageStickerAttachmentController Instance = new();

    private TelegramMessageStickerAttachmentController() { }

    public IMessageStickerAttachmentFactory Build(IMessageAttachmentIdentifier identifier)
    {
        if (identifier is not TelegramMessageFileAttachmentIdentifier fileIdentifier)
        {
            throw new NotSupportedException();
        }

        var localFileIdentifier = fileIdentifier.LocalFileIdentifier;

        ArgumentException.ThrowIfNullOrWhiteSpace(localFileIdentifier, nameof(localFileIdentifier));

        return new TelegramMessageStickerAttachmentFactory(localFileIdentifier);
    }
}
