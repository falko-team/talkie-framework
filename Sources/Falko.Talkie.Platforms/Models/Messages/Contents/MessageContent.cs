using Talkie.Common;

namespace Talkie.Models.Messages.Contents;

public readonly record struct MessageContent(string Text, IReadOnlyCollection<IMessageTextStyle> Styles)
{
    public static readonly MessageContent Empty = new(string.Empty, []);

    public MessageContent(string text) : this(text, []) { }

    public bool IsEmpty => Text.IsNullOrEmpty();

    public override string ToString() => Text;

    public static implicit operator string(MessageContent content) => content.Text;

    public static implicit operator MessageContent(string text) => new(text);
}
