using Talkie.Connections;
using Talkie.Signals.Mixins;

namespace Talkie.Signals;

public sealed record UnobservedConnectionExceptionSignal(ISignalConnection Connection, Exception Exception)
    : Signal, IWithUnobservedExceptionSignal
{
    public static implicit operator Exception(UnobservedConnectionExceptionSignal signal)
    {
        return signal.Exception;
    }
}
