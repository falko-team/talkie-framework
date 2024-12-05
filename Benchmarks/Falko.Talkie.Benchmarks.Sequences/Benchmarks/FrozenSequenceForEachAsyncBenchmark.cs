using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Talkie.Models;
using Talkie.Sequences;

namespace Talkie.Benchmarks.Benchmarks;

[SimpleJob(RunStrategy.Throughput)]
public class FrozenSequenceForEachAsyncBenchmark
{
    private const int IterationsCount = 10;

    private readonly FrozenSequence<Reference> _frozenSequence = new(Enumerable.Repeat(Reference.Shared, IterationsCount));

    [Benchmark]
    public async Task FrozenSequenceEnumerableWrapperForEach()
    {
        foreach (var reference in _frozenSequence.AsEnumerable()) _ = reference;

        await Task.CompletedTask;
    }

    [Benchmark]
    public async Task FrozenSequenceEnumerableCastForEach()
    {
        foreach (var reference in (IEnumerable<Reference>)_frozenSequence) _ = reference;

        await Task.CompletedTask;
    }

    [Benchmark]
    public async Task FrozenSequenceExtensionsForEach()
    {
        _frozenSequence.ForEach(reference => _ = reference);

        await Task.CompletedTask;
    }
}
