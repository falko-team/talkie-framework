using Talkie.Flows;
using Talkie.Signals;

namespace Talkie.Handlers;

public sealed class SignalContext(ISignalFlow flow, Signal signal) : ISignalContext
{
    public ISignalFlow Flow { get; } = flow;

    public Signal Signal { get; } = signal;
}
