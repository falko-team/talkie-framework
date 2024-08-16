using Talkie.Exceptions;
using Talkie.Signals.Mixins;

namespace Talkie.Signals;

public sealed record UnobservedPublishingExceptionSignal(SignalPublishingException Exception)
    : Signal, IWithUnobservedExceptionSignal
{
    Exception IWithUnobservedExceptionSignal.Exception => Exception;

    public static implicit operator SignalPublishingException(UnobservedPublishingExceptionSignal signal)
        => signal.Exception;
}
