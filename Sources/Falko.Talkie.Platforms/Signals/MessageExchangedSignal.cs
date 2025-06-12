using Falko.Talkie.Models.Messages.Incoming;

namespace Falko.Talkie.Signals;

public sealed record MessageExchangedSignal(IIncomingMessage Message) : Signal, IWithIncomingMessageSignal;
