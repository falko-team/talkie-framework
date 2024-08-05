using Talkie.Signals;

namespace Talkie.Models.Messages;

public static partial class IncomingMessageExtensions
{
    public static IOutgoingMessage ToOutgoingMessage(this IIncomingMessage message)
    {
        return new OutgoingMessage
        {
            Text = message.Text,
            Reply = message.Reply?.GetGlobalIdentifier()
        };
    }

    public static IOutgoingMessage ToOutgoingMessage(this IMessage message)
    {
        return new OutgoingMessage
        {
            Text = message.Text
        };
    }

    public static GlobalIdentifier GetGlobalIdentifier(this IIncomingMessage message)
    {
        return new GlobalIdentifier(message.EnvironmentProfile.Identifier, message.Identifier);
    }

    public static IncomingMessageSignal ToSignal(this IIncomingMessage message)
    {
        return new IncomingMessageSignal(message);
    }

    public static IIncomingMessage Mutate(this IIncomingMessage message,
        Func<IIncomingMessageMutator, IIncomingMessageMutator> mutationFactory)
    {
        return mutationFactory(message.ToMutator()).Mutate();
    }
}
