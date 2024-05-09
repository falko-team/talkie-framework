using Falko.Unibot.Signals;

namespace Falko.Unibot.Handlers;

public interface ISignalContext<out T> : ISignalContext where T : Signal
{
    new T Signal { get; }
}
