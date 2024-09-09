using Talkie.Models.Messages.Incoming;

namespace Talkie.Signals;

public sealed record MessagePublishedSignal(IIncomingMessage Message) : Signal, IWithIncomingMessageSignal;
