using Talkie.Connections;
using Talkie.Signals.Mixins;

namespace Talkie.Signals;

/// <summary>
/// Represents a <see cref="Signal"/> that indicates an connection unobserved exception.
/// </summary>
/// <param name="Connection">The connection that caused the exception.</param>
/// <param name="Exception"><inheritdoc cref="IWithUnobservedExceptionSignal.Exception"/></param>
public sealed record UnobservedConnectionExceptionSignal(ISignalConnection Connection, Exception Exception)
    : Signal, IWithUnobservedExceptionSignal
{
    /// <summary>
    /// Implicitly converts a <see cref="UnobservedConnectionExceptionSignal"/> to a <see cref="Exception"/>.
    /// </summary>
    /// <param name="signal">The signal to convert.</param>
    /// <returns>The <see cref="Exception"/> from the signal.</returns>
    public static implicit operator Exception(UnobservedConnectionExceptionSignal signal)
    {
        return signal.Exception;
    }
}
