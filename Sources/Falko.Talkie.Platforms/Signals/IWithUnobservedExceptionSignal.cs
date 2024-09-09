using Talkie.Mixins;

namespace Talkie.Signals;

/// <summary>
/// The mixin for <see cref="Signal"/> that have an unobserved exception.
/// </summary>
public interface IWithUnobservedExceptionSignal : IWith<Signal>
{
    /// <summary>
    /// Gets the unobserved exception.
    /// </summary>
    Exception Exception { get; }
}
