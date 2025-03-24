using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments.Factories;

namespace Talkie.Controllers.AttachmentControllers;

public sealed class TelegramMessageImageAttachmentController : IMessageImageAttachmentController
{
    public static readonly TelegramMessageImageAttachmentController Instance = new();

    private TelegramMessageImageAttachmentController() { }

    public IMessageImageAttachmentFactory Build(IMessageAttachmentIdentifier identifier)
    {
        if (identifier is not TelegramMessageFileAttachmentIdentifier fileIdentifier)
        {
            throw new NotSupportedException();
        }

        var localFileIdentifier = fileIdentifier.LocalFileIdentifier;

        ArgumentException.ThrowIfNullOrWhiteSpace(localFileIdentifier, nameof(localFileIdentifier));

        return new TelegramMessageImageAttachmentFactory { Alias = localFileIdentifier };
    }

    public IMessageImageAttachmentFactory Build(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        if (uri.IsFile)
        {
            var filePath = uri.LocalPath;

            var fileStream = File.OpenRead(filePath);

            var fileName = Path.GetFileName(filePath);

            return new TelegramMessageImageAttachmentFactory { Alias = fileName, Stream = fileStream };
        }

        if (uri.Scheme is "http" or "https")
        {
            return new TelegramMessageImageAttachmentFactory { Alias = uri.AbsoluteUri };
        }

        throw new NotSupportedException();
    }

    public IMessageImageAttachmentFactory Build(Stream stream)
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

        if (stream is not FileStream fileStream)
        {
            return new TelegramMessageImageAttachmentFactory { Stream = stream };
        }

        var fileName = Path.GetFileName(fileStream.Name);

        return new TelegramMessageImageAttachmentFactory { Alias = fileName, Stream = fileStream };
    }
}
