using Talkie.Exceptions;
using Talkie.Signals.Mixins;

namespace Talkie.Signals;

/// <summary>
/// Represents a <see cref="Signal"/> that indicates an unobserved exception.
/// </summary>
/// <param name="Exception"><inheritdoc cref="IWithUnobservedExceptionSignal.Exception"/></param>
public sealed record UnobservedPublishingExceptionSignal(SignalPublishingException Exception)
    : Signal, IWithUnobservedExceptionSignal
{
    /// <inheritdoc />
    Exception IWithUnobservedExceptionSignal.Exception => Exception;

    /// <summary>
    /// Implicitly converts a <see cref="UnobservedPublishingExceptionSignal"/> to a <see cref="SignalPublishingException"/>.
    /// </summary>
    /// <param name="signal">The signal to convert.</param>
    /// <returns>The <see cref="SignalPublishingException"/> from the signal.</returns>
    public static implicit operator SignalPublishingException(UnobservedPublishingExceptionSignal signal)
        => signal.Exception;
}
