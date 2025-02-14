using BenchmarkDotNet.Attributes;
using Talkie.Models;
using Talkie.Sequences;

namespace Talkie.Benchmarks.Benchmarks;

public class FrozenSequenceVsListGettingByIndexBenchmark
{
    private const int IterationsCount = 10;

    private readonly FrozenSequence<Reference> _frozenSequence = Enumerable
        .Repeat(Reference.Shared, IterationsCount)
        .ToFrozenSequence();

    private readonly List<Reference> _list = Enumerable
        .Repeat(Reference.Shared, IterationsCount)
        .ToList();

    [Benchmark]
    public void FrozenSequenceIndexerGettingByIndex()
    {
        var s = _frozenSequence.AsIndexer();

        for (var i = 0; i < IterationsCount; i++)
        {
            _ = s[i];
        }
    }

    [Benchmark]
    public void FrozenSequenceSpanGettingByIndex()
    {
        var s = _frozenSequence.AsSpan();

        for (var i = 0; i < IterationsCount; i++)
        {
            _ = s[i];
        }
    }

    [Benchmark]
    public void FrozenSequenceDirectGettingByIndex()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            _ = _frozenSequence[i];
        }
    }

    [Benchmark]
    public void ListGettingByIndex()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            _ = _list[i];
        }
    }
}
