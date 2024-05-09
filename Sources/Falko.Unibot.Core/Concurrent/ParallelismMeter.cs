namespace Falko.Unibot.Concurrent;

public sealed class ParallelismMeter
{
    private int _parallels = 1;

    private long _duration;

    public int Parallels => _parallels;

    public void Measure(long durationTicks, int maximumParallels = int.MaxValue)
    {
        if (_parallels >= maximumParallels) return;

        if (durationTicks <= 0) return;

        var previousDuration = _duration;

        if (previousDuration > 0 && previousDuration < durationTicks) return;

        Interlocked.CompareExchange(ref _duration, durationTicks, previousDuration);

        Interlocked.Increment(ref _parallels);
    }
}
