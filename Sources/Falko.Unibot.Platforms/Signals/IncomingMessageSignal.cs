using Falko.Unibot.Models.Messages;

namespace Falko.Unibot.Signals;

public sealed record IncomingMessageSignal(IMessage Message) : Signal;
