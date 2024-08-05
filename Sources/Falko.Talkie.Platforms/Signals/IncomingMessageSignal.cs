using Talkie.Models.Messages;

namespace Talkie.Signals;

public sealed record IncomingMessageSignal(IIncomingMessage Message) : Signal;
