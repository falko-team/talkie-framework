using Falko.Unibot.Models.Profiles;
using Falko.Unibot.Platforms;

namespace Falko.Unibot.Models.Messages;

public sealed record TelegramIncomingMessage : IMessage, XMessage.IWithIdentifier, XMessage.IWithPlatform, XMessage.IWithEntry
{
    public required Identifier Id { get; init; }

    public required IPlatform Platform { get; init; }

    public required IProfile Sender { get; init; }

    public required DateTime Sent { get; init; }

    public required IProfile Receiver { get; init; }

    public required DateTime? Received { get; init; }

    public string? Content { get; init; }
}
