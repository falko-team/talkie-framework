using Talkie.Models.Identifiers;

namespace Talkie.Models.Messages.Attachments.Variants;

public sealed record MessageImageVariant(Func<CancellationToken, Task<Stream>> streamFactory) : IMessageImageVariant
{
    public required IMessageAttachmentIdentifier Identifier { get; init; }

    public string? Name { get; init; }

    public string? Type { get; init; }

    public Area Area { get; init; }

    public long Size { get; init; }

    public Task<Stream> ToStreamAsync(CancellationToken cancellationToken = default) => streamFactory(cancellationToken);
}
