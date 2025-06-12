using Falko.Talkie.Models.Identifiers;
using Falko.Talkie.Models.Messages.Attachments;
using Falko.Talkie.Models.Messages.Contents;
using Falko.Talkie.Models.Messages.Features;
using Falko.Talkie.Models.Profiles;
using Falko.Talkie.Platforms;
using Falko.Talkie.Sequences;

namespace Falko.Talkie.Models.Messages.Incoming;

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
