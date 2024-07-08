using Talkie.Exceptions;

namespace Talkie.Signals;

public sealed record UnobservedPublishingExceptionSignal(SignalPublishingException Exception) : Signal
{
    public static implicit operator SignalPublishingException(UnobservedPublishingExceptionSignal signal)
    {
        return signal.Exception;
    }
}
