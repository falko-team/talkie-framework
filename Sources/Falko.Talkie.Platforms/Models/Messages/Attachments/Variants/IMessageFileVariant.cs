using Falko.Talkie.Models.Identifiers;

namespace Falko.Talkie.Models.Messages.Attachments.Variants;

public interface IMessageFileVariant
{
    IMessageAttachmentIdentifier Identifier { get; }

    string? Name { get; }

    string? Type { get; }

    long Size { get; }

    Task<Stream> ToStreamAsync(CancellationToken cancellationToken = default);
}
