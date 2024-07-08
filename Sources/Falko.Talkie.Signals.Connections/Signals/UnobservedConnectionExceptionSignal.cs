using Talkie.Connections;

namespace Talkie.Signals;

public sealed record UnobservedConnectionExceptionSignal(ISignalConnection Connection, Exception Exception) : Signal
{
    public static implicit operator Exception(UnobservedConnectionExceptionSignal signal)
    {
        return signal.Exception;
    }
}
