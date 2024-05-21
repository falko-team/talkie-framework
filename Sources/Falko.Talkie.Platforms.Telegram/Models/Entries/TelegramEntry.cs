using Talkie.Models.Profiles;

namespace Talkie.Models.Entries;

public sealed record TelegramEntry : IEntry
{
    public required IProfile Sender { get; init; }

    public required DateTime Sent { get; init; }

    public required IProfile Receiver { get; init; }

    public required DateTime Received { get; init; }

    public required IProfile Environment { get; init; }
}
