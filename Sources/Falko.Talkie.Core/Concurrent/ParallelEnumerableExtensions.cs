using System.Collections.Concurrent;
using Talkie.Sequences;

namespace Talkie.Concurrent;

public static partial class ParallelEnumerableExtensions
{
    public static Partitioner<T> ToPartitioner<T>(this IParallelEnumerable<T> parallelEnumerable)
    {
        return new ParallelEnumerablePartitioner<T>(parallelEnumerable);
    }

    public static SequenceParallelismCoordinator<T>.Static Parallelize<T>(this IReadOnlySequence<T> sequence)
    {
        return new SequenceParallelismCoordinator<T>.Static(sequence);
    }

    public static SequenceParallelismCoordinator<T>.Dynamic Parallelize<T>(this IReadOnlySequence<T> sequence, ParallelismMeter meter)
    {
        return new SequenceParallelismCoordinator<T>.Dynamic(sequence, meter);
    }

    public static Task ForEachAsync<T>(this SequenceParallelismCoordinator<T>.Static parallel,
        Func<T, CancellationToken, ValueTask> @do,
        CancellationToken cancellationToken = default)
    {
        return Parallel.ForEachAsync(parallel.Sequence, cancellationToken, @do);
    }

    public static async Task ForEachAsync<T>(this SequenceParallelismCoordinator<T>.Dynamic parallel,
        Func<T, CancellationToken, ValueTask> @do,
        CancellationToken cancellationToken = default)
    {
        var startTicks = Environment.TickCount64;

        var parallels = parallel.Meter.CurrentParallels;

        if (parallels is 1)
        {
            foreach (var item in parallel.Sequence)
            {
                await @do(item, cancellationToken);
            }
        }
        else
        {
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = parallels,
                CancellationToken = cancellationToken
            };

            await Parallel.ForEachAsync(parallel.Sequence, options, @do);
        }

        var endTicks = Environment.TickCount64;

        parallel.Meter.Measure(endTicks - startTicks, parallel.Sequence.Count);
    }

    public static void ForEach<T>(this SequenceParallelismCoordinator<T>.Static parallel, Action<T, CancellationToken> @do,
        CancellationToken cancellationToken = default)
    {
        var options = new ParallelOptions
        {
            CancellationToken = cancellationToken
        };

        Parallel.ForEach(parallel.Sequence.ToPartitioner(), options, item => @do(item, cancellationToken));
    }

    public static void ForEach<T>(this SequenceParallelismCoordinator<T>.Dynamic parallel, Action<T, CancellationToken> @do,
        CancellationToken cancellationToken = default)
    {
        var startTicks = Environment.TickCount64;

        var parallels = parallel.Meter.CurrentParallels;

        if (parallels is 1)
        {
            foreach (var item in parallel.Sequence)
            {
                @do(item, cancellationToken);
            }
        }
        else
        {
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = parallels,
                CancellationToken = cancellationToken
            };

            Parallel.ForEach(parallel.Sequence.ToPartitioner(), options, item => @do(item, cancellationToken));
        }

        var endTicks = Environment.TickCount64;

        parallel.Meter.Measure(endTicks - startTicks, parallel.Sequence.Count);
    }
}
