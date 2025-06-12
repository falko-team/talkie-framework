using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public interface ISignalContext<out T> : ISignalContext where T : Signal
{
    new T Signal { get; }
}
