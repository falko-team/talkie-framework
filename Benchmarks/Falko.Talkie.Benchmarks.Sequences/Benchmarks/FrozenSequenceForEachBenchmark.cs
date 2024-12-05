using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Talkie.Models;
using Talkie.Sequences;

namespace Talkie.Benchmarks.Benchmarks;

[SimpleJob(RunStrategy.Throughput)]
public class FrozenSequenceForEachBenchmark
{
    private const int IterationsCount = 10;

    private readonly FrozenSequence<Reference> _frozenSequence = new(Enumerable.Repeat(Reference.Shared, IterationsCount));

    [Benchmark]
    public void FrozenSequenceStructForEach()
    {
        foreach (var reference in _frozenSequence) _ = reference;
    }

    [Benchmark]
    public void FrozenSequenceEnumerableWrapperForEach()
    {
        foreach (var reference in _frozenSequence.AsEnumerable()) _ = reference;
    }

    [Benchmark]
    public void FrozenSequenceEnumerableCastForEach()
    {
        foreach (var reference in (IEnumerable<Reference>)_frozenSequence) _ = reference;
    }

    [Benchmark]
    public void FrozenSequenceMemoryForEach()
    {
        foreach (var reference in _frozenSequence.AsMemory().Span) _ = reference;
    }

    [Benchmark]
    public void FrozenSequenceSpanForEach()
    {
        foreach (var reference in _frozenSequence.AsSpan()) _ = reference;
    }

    [Benchmark]
    public void FrozenSequenceExtensionsForEach()
    {
        _frozenSequence.ForEach(reference => _ = reference);
    }
}
