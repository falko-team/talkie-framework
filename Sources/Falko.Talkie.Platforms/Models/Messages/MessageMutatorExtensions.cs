using Talkie.Models.Messages.Contents;

namespace Talkie.Models.Messages;

public static partial class MessageMutatorExtensions
{
    /// <summary>
    /// Mutates the content of the message using the specified content mutation factory.
    /// The factory receives the current content and a new instance of builder to create a new content.
    /// </summary>
    /// <param name="mutator">The mutator.</param>
    /// <param name="contentMutationFactory">The content mutation factory.</param>
    /// <typeparam name="TMutator">The type of the mutator.</typeparam>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    /// <returns>The incoming mutator.</returns>
    public static TMutator MutateContent<TMutator, TMessage>(this IMessageMutator<TMutator, TMessage> mutator,
            Func<MessageContent, IMessageContentBuilder, IMessageContentBuilder> contentMutationFactory)
        where TMutator : IMessageMutator<TMutator, TMessage>
        where TMessage : IMessage
    {
        return mutator.MutateContent(content => contentMutationFactory(content, new MessageContentBuilder()).Build());
    }
}
