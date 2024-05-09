using Falko.Unibot.Flows;
using Falko.Unibot.Signals;

namespace Falko.Unibot.Handlers;

public sealed class SignalContext(ISignalFlow flow, Signal signal) : ISignalContext
{
    public ISignalFlow Flow { get; } = flow;

    public Signal Signal { get; } = signal;
}
