using Falko.Talkie.Flows;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public sealed class SignalContext<T>(ISignalFlow flow, T signal) : ISignalContext<T> where T : Signal
{
    public ISignalFlow Flow { get; } = flow;

    public T Signal { get; } = signal;

    Signal ISignalContext.Signal => Signal;
}
