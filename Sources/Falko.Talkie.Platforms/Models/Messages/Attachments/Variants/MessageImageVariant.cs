using System.Runtime.Serialization;
using Talkie.Models.Identifiers;

namespace Talkie.Models.Messages.Attachments.Variants;

public sealed record MessageImageVariant : IMessageImageVariant
{
    [IgnoreDataMember]
    private readonly Func<CancellationToken, Task<Stream>> _streamFactory;

    public MessageImageVariant(Func<CancellationToken, Task<Stream>> streamFactory)
    {
        _streamFactory = streamFactory;
    }

    public required IMessageAttachmentIdentifier Identifier { get; init; }

    public string? Name { get; init; }

    public string? Type { get; init; }

    public Area Area { get; init; }

    public long Size { get; init; }

    public Task<Stream> ToStreamAsync(CancellationToken cancellationToken = default)
    {
        return _streamFactory(cancellationToken);
    }
}
