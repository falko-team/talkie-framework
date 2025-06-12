using Falko.Talkie.Mixins;
using Falko.Talkie.Models.Messages.Incoming;

namespace Falko.Talkie.Signals;

public interface IWithIncomingMessageSignal : IWith<Signal>
{
    IIncomingMessage Message { get; }
}
