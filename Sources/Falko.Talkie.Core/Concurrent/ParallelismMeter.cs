namespace Talkie.Concurrent;

public sealed class ParallelismMeter
{
    private int _parallels;

    private long _duration;

    public ParallelismMeter(int minimumParallels = 1, int maximumParallels = int.MaxValue)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(minimumParallels, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(maximumParallels, minimumParallels);

        _parallels = minimumParallels;

        maximumParallels = Math.Min(maximumParallels, Environment.ProcessorCount);

        MinimumParallels = minimumParallels;
        MaximumParallels = maximumParallels;
    }

    public int MinimumParallels { get; }

    public int MaximumParallels { get; }

    public int CurrentParallels => _parallels;

    public void Measure(long durationTicks, int maximumParallels = int.MaxValue)
    {
        maximumParallels = Math.Min(maximumParallels, MaximumParallels);

        if (_parallels >= maximumParallels) return;

        if (durationTicks <= 0) return;

        var previousDuration = _duration;

        if (previousDuration > 0 && previousDuration < durationTicks) return;

        Interlocked.CompareExchange(ref _duration, durationTicks, previousDuration);

        Interlocked.Increment(ref _parallels);
    }
}
