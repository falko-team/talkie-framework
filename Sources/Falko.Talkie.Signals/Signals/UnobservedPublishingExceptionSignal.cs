using Talkie.Exceptions;

namespace Talkie.Signals;

public sealed record UnobservedPublishingExceptionSignal(SignalPublishingException Exception) : Signal
{
    public static implicit operator UnobservedPublishingExceptionSignal(SignalPublishingException exception)
    {
        return new UnobservedPublishingExceptionSignal(exception);
    }

    public static implicit operator SignalPublishingException(UnobservedPublishingExceptionSignal signal)
    {
        return signal.Exception;
    }
}
