namespace Falko.Talkie.Concurrent;

/// <summary>
/// Represents a parallelism meter.
/// The parallelism meter should measure duration of a code
/// and after trying to find the optimal number of parallels
/// between <see cref="MinimumParallels"/> and <see cref="MaximumParallels"/>.
/// </summary>
public interface IParallelismMeter
{
    /// <summary>
    /// Gets the minimum number of parallels.
    /// </summary>
    int MinimumParallels { get; }

    /// <summary>
    /// Gets the maximum number of parallels.
    /// </summary>
    int MaximumParallels { get; }

    /// <summary>
    /// Gets the actual (current) number of parallels.
    /// Between or equal to <see cref="MinimumParallels"/> and <see cref="MaximumParallels"/>.
    /// </summary>
    int CurrentParallels { get; }

    /// <summary>
    /// Recalculates the <see cref="CurrentParallels"/> based on the duration of execution of the code.
    /// </summary>
    /// <param name="durationTicks">The duration of execution of the code in ticks.</param>
    /// <param name="maximumParallels">The maximum number of parallels to use in this measurement.</param>
    void Measure(long durationTicks, int maximumParallels = int.MaxValue);
}
