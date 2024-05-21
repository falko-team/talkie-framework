using System.Collections.Concurrent;
using Falko.Talkie.Collections;

namespace Falko.Talkie.Concurrent;

public static class IterableExtensions
{
    public static Partitioner<T> ToPartitioner<T>(this IIterable<T> iterable)
    {
        return new IterablePartitioner<T>(iterable);
    }

    public static SequenceParallelismCoordinator<T>.Static Parallelize<T>(this IReadOnlySequence<T> sequence)
    {
        return new SequenceParallelismCoordinator<T>.Static(sequence);
    }

    public static SequenceParallelismCoordinator<T>.Dynamic Parallelize<T>(this IReadOnlySequence<T> sequence, ParallelismMeter meter)
    {
        return new SequenceParallelismCoordinator<T>.Dynamic(sequence, meter);
    }

    public static Task ForEachAsync<T>(this SequenceParallelismCoordinator<T>.Static parallel, Action<T, CancellationToken> @do,
        CancellationToken cancellationToken = default)
    {
        return Parallel.ForEachAsync(parallel.Sequence, cancellationToken, (item, scopedCancellationToken) =>
        {
            @do(item, scopedCancellationToken);

            return ValueTask.CompletedTask;
        });
    }

    public static async Task ForEachAsync<T>(this SequenceParallelismCoordinator<T>.Dynamic parallel, Action<T, CancellationToken> @do,
        CancellationToken cancellationToken = default)
    {
        var startTicks = Environment.TickCount64;

        var parallels = parallel.Meter.Parallels;

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

            await Parallel.ForEachAsync(parallel.Sequence, options, (item, scopedCancellationToken) =>
            {
                @do(item, scopedCancellationToken);

                return ValueTask.CompletedTask;
            });
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

        var parallels = parallel.Meter.Parallels;

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
