using Falko.Talkie.Flows;
using Falko.Talkie.Signals;

namespace Falko.Talkie.Handlers;

public sealed class SignalContext(ISignalFlow flow, Signal signal) : ISignalContext
{
    public ISignalFlow Flow { get; } = flow;

    public Signal Signal { get; } = signal;
}
