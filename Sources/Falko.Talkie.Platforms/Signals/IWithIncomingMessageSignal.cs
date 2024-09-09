using Talkie.Mixins;
using Talkie.Models.Messages.Incoming;

namespace Talkie.Signals;

public interface IWithIncomingMessageSignal : IWith<Signal>
{
    IIncomingMessage Message { get; }
}
