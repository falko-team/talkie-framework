using Talkie.Models.Entries;
using Talkie.Platforms;

namespace Talkie.Models.Messages;

public sealed record TelegramIncomingMessage : IIncomingMessage
{
    public required Identifier Id { get; init; }

    public required TelegramEntry Entry { get; init; }

    IEntry Message.IWithEntry.Entry => Entry;

    public required TelegramPlatform Platform { get; init; }

    IPlatform Message.IWithPlatform.Platform => Platform;

    public string? Content { get; init; }
}
