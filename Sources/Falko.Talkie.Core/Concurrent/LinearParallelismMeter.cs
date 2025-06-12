namespace Falko.Talkie.Concurrent;

/// <summary>
/// The linear parallelism meter implementation.
/// <inheritdoc cref="IParallelismMeter"/>
/// </summary>
public sealed class LinearParallelismMeter : IParallelismMeter
{
    private int _currentParallels;

    private long _lastDurationTicks;

    public LinearParallelismMeter(int minimumParallels = 1, int maximumParallels = int.MaxValue)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(minimumParallels, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(maximumParallels, minimumParallels);

        _currentParallels = minimumParallels;

        maximumParallels = Math.Min(maximumParallels, Environment.ProcessorCount);

        MinimumParallels = minimumParallels;
        MaximumParallels = maximumParallels;
    }

    public int MinimumParallels { get; }

    public int MaximumParallels { get; }

    public int CurrentParallels => _currentParallels;

    public void Measure(long durationTicks, int maximumParallels = int.MaxValue)
    {
        maximumParallels = Math.Min(maximumParallels, MaximumParallels);

        if (_currentParallels >= maximumParallels) return;

        if (durationTicks <= 0) return;

        var previousDuration = _lastDurationTicks;

        if (previousDuration > 0 && previousDuration < durationTicks) return;

        Interlocked.CompareExchange(ref _lastDurationTicks, durationTicks, previousDuration);

        Interlocked.Increment(ref _currentParallels);
    }
}
