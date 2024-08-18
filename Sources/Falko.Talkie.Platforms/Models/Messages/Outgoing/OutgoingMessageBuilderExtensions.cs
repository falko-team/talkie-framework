using Talkie.Collections;
using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages.Outgoing;

public static partial class OutgoingMessageBuilderExtensions
{
    public static IOutgoingMessageBuilder AddContent(this IOutgoingMessageBuilder builder,
        MessageContent content)
    {
        var previousContext = builder.Content;

        var newContext = new MessageContent(previousContext.Text + content.Text,
            previousContext.Styles.Concat(content.Styles).ToFrozenSequence());

        return builder.SetContent(newContext);
    }

    public static IOutgoingMessageBuilder SetContent(this IOutgoingMessageBuilder builder,
        IMessageContentBuilder contentBuilder)
    {
        return builder.SetContent(contentBuilder.Build());
    }

    public static IOutgoingMessageBuilder AddContent(this IOutgoingMessageBuilder builder,
        IMessageContentBuilder contentBuilder)
    {
        return builder.AddContent(contentBuilder.Build());
    }

    public static IOutgoingMessageBuilder SetContent(this IOutgoingMessageBuilder builder,
        Func<IMessageContentBuilder, IMessageContentBuilder> contentBuilderFactory)
    {
        return builder.SetContent(contentBuilderFactory(new MessageContentBuilder()).Build());
    }

    public static IOutgoingMessageBuilder AddContent(this IOutgoingMessageBuilder builder,
        Func<IMessageContentBuilder, IMessageContentBuilder> contentBuilderFactory)
    {
        return builder.AddContent(contentBuilderFactory(new MessageContentBuilder()).Build());
    }
}
