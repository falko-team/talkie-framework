using Talkie.Flows;
using Talkie.Signals;

namespace Talkie.Exceptions;

public sealed class SignalPublishingException(ISignalFlow flow, Signal signal, Exception innerException)
    : Exception(innerException.Message, innerException)
{
    public ISignalFlow Flow { get; } = flow;

    public Signal Signal { get; } = signal;
}
