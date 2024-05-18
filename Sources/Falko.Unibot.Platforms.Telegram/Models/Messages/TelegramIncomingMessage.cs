using Falko.Unibot.Models.Entries;
using Falko.Unibot.Platforms;

namespace Falko.Unibot.Models.Messages;

public sealed record TelegramIncomingMessage : IIncomingMessage
{
    public required Identifier Id { get; init; }

    public required TelegramEntry Entry { get; init; }

    IEntry Message.IWithEntry.Entry => Entry;

    public required TelegramPlatform Platform { get; init; }

    IPlatform Message.IWithPlatform.Platform => Platform;

    public string? Content { get; init; }
}
