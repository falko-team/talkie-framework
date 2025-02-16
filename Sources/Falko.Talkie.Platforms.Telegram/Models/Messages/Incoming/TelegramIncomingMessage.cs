using Talkie.Models.Identifiers;
using Talkie.Models.Messages.Attachments;
using Talkie.Models.Messages.Contents;
using Talkie.Models.Messages.Features;
using Talkie.Models.Profiles;
using Talkie.Platforms;
using Talkie.Sequences;

namespace Talkie.Models.Messages.Incoming;

public sealed record TelegramIncomingMessage : IIncomingMessage
{
    public required IMessageIdentifier Identifier { get; init; }

    public required IPlatform Platform { get; init; }

    public required IProfile EnvironmentProfile { get; init; }

    public required IProfile PublisherProfile { get; init; }

    public required IProfile ReceiverProfile { get; init; }

    public required DateTime PublishedDate { get; init; }

    public required DateTime ReceivedDate { get; init; }

    public IEnumerable<IMessageFeature> Features { get; init; }
        = FrozenSequence.Empty<IMessageFeature>();

    public MessageContent Content { get; init; }
        = MessageContent.Empty;

    public TelegramIncomingMessage? Reply { get; init; }

    public IEnumerable<IMessageAttachment> Attachments { get; init; }
        = FrozenSequence.Empty<IMessageAttachment>();

    IIncomingMessage? IIncomingMessage.Reply => Reply;

    public TelegramIncomingMessageMutator ToMutator() => new(this);

    IIncomingMessageMutator IIncomingMessage.ToMutator() => ToMutator();
}
