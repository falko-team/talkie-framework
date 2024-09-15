using Talkie.Common;
using Talkie.Models.Messages.Contents.Styles;

namespace Talkie.Models.Messages.Contents;

public readonly record struct MessageContent(string Text, IReadOnlyCollection<IMessageTextStyle> Styles)
{
    public static readonly MessageContent Empty = new(string.Empty, []);

    public MessageContent(string text) : this(text, []) { }

    public bool IsEmpty => Text.IsNullOrEmpty();

    public static implicit operator string(MessageContent content) => content.Text;

    public static implicit operator MessageContent(string text) => new(text);
}
