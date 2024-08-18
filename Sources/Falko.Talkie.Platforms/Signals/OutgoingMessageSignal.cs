using Talkie.Models.Messages.Outgoing;

namespace Talkie.Signals;

public sealed record OutgoingMessageSignal(IOutgoingMessage Message) : Signal;
