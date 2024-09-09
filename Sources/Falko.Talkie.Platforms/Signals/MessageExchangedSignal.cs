using Talkie.Models.Messages.Incoming;

namespace Talkie.Signals;

public sealed record MessageExchangedSignal(IIncomingMessage Message) : Signal, IWithIncomingMessageSignal;
