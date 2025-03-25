using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramMessageAudioAttachmentController : IMessageAudioAttachmentController
{
    public static readonly TelegramMessageAudioAttachmentController Instance = new();

    private TelegramMessageAudioAttachmentController() { }

    public IMessageAudioAttachmentFactory Build
    (
        IMessageAttachmentIdentifier identifier,
        MessageAudioAttachmentMetadata metadata = default
    )
    {
        if (identifier is not TelegramMessageFileAttachmentIdentifier fileIdentifier)
        {
            throw new NotSupportedException();
        }

        var localFileIdentifier = fileIdentifier.LocalFileIdentifier;

        ArgumentException.ThrowIfNullOrWhiteSpace(localFileIdentifier, nameof(localFileIdentifier));

        return new TelegramMessageAudioAttachmentFactory
        {
            Alias = localFileIdentifier,
            Title = metadata.Title,
            Performer = metadata.Performer,
            Duration = metadata.Duration,
            Thumbnail = metadata.Thumbnail as TelegramMessageImageAttachmentFactory
        };
    }

    public IMessageAudioAttachmentFactory Build
    (
        Uri uri,
        MessageAudioAttachmentMetadata metadata = default
    )
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        if (uri.IsFile is false) throw new NotSupportedException();

        var filePath = uri.LocalPath;

        var fileStream = File.OpenRead(filePath);

        return new TelegramMessageAudioAttachmentFactory
        {
            Stream = fileStream,
            Title = metadata.Title,
            Performer = metadata.Performer,
            Duration = metadata.Duration,
            Thumbnail = metadata.Thumbnail as TelegramMessageImageAttachmentFactory
        };
    }

    public IMessageAudioAttachmentFactory Build
    (
        Stream stream,
        MessageAudioAttachmentMetadata metadata = default
    )
    {
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));

        if (stream.CanRead is false)
        {
            throw new ArgumentException("Stream is not readable.");
        }

        if (stream.Position is not 0)
        {
            throw new ArgumentException("Stream is not at the beginning.");
        }

        return new TelegramMessageAudioAttachmentFactory
        {
            Stream = stream,
            Title = metadata.Title,
            Performer = metadata.Performer,
            Duration = metadata.Duration,
            Thumbnail = metadata.Thumbnail as TelegramMessageImageAttachmentFactory
        };
    }
}
