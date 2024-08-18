using Talkie.Models.Messages.Incoming;

namespace Talkie.Signals;

public sealed record IncomingMessageSignal(IIncomingMessage Message) : Signal;
