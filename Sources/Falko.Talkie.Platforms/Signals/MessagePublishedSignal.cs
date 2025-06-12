using Falko.Talkie.Models.Messages.Incoming;

namespace Falko.Talkie.Signals;

public sealed record MessagePublishedSignal(IIncomingMessage Message) : Signal, IWithIncomingMessageSignal;
