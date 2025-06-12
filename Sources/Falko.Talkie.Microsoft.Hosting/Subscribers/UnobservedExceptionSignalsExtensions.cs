using Falko.Talkie.Signals;

namespace Falko.Talkie.Subscribers;

internal static class UnobservedExceptionSignalsExtensions
{
    public static bool IsUnobservedExceptionSignal(this Signal signal)
    {
        return signal is IWithUnobservedExceptionSignal;
    }

    public static Exception? GetUnobservedExceptionSignalException(this Signal signal)
    {
        return (signal as IWithUnobservedExceptionSignal)?.Exception;
    }
}
