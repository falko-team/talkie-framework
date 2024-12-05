using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Talkie.Models;
using Talkie.Sequences;

namespace Talkie.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Throughput)]
public class SequencesVsOtherFirstAndLastBenchmark
{
    private const int Capacity = 10;

    private FrozenSequence<Reference>? _frozenSequence;

    private IEnumerable<Reference>? _referenceFrozenSequence;

    private RemovableSequence<Reference>? _removableSequence;

    private Sequence<Reference>? _sequence;

    private LinkedList<Reference>? _linkedList;

    private List<Reference>? _list;

    [GlobalSetup]
    public void Setup()
    {
        SetupFrozenSequence();
        SetupRemovableSequence();
        SetupSequence();
        SetupLinkedList();
        SetupList();
    }

    public void SetupFrozenSequence()
    {
        var sequence = new List<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            sequence.Add(Reference.Shared);
        }

        _frozenSequence = sequence.ToFrozenSequence();
        _referenceFrozenSequence = _frozenSequence;
    }

    public void SetupRemovableSequence()
    {
        var removableSequence = new RemovableSequence<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            _ = removableSequence.Add(Reference.Shared);
        }

        _removableSequence = removableSequence;
    }

    public void SetupSequence()
    {
        var sequence = new Sequence<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            sequence.Add(Reference.Shared);
        }

        _sequence = sequence;
    }

    public void SetupLinkedList()
    {
        var linkedList = new LinkedList<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            _ = linkedList.AddLast(Reference.Shared);
        }

        _linkedList = linkedList;
    }

    public void SetupList()
    {
        var list = new List<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            list.Add(Reference.Shared);
        }

        _list = list;
    }

    [Benchmark]
    public void RunFrozenSequenceFirstAndLast()
    {
        _ = _frozenSequence!.First();
        _ = _frozenSequence!.Last();
    }

    [Benchmark]
    public void RunReferenceFrozenSequenceFirstAndLast()
    {
        _ = _referenceFrozenSequence!.First();
        _ = _referenceFrozenSequence!.Last();
    }

    [Benchmark]
    public void RunReferenceSequencingFrozenSequenceFirstAndLast()
    {
        _ = _referenceFrozenSequence!.Sequencing().First();
        _ = _referenceFrozenSequence!.Sequencing().Last();
    }

    [Benchmark]
    public void RunRemovableSequenceFirstAndLast()
    {
        _ = _removableSequence!.First();
        _ = _removableSequence!.Last();
    }

    [Benchmark]
    public void RunSequenceFirstAndLast()
    {
        _ = _sequence!.First();
        _ = _sequence!.Last();
    }

    [Benchmark]
    public void RunLinkedListFirstAndLast()
    {
        _ = _linkedList!.First();
        _ = _linkedList!.Last();
    }

    [Benchmark]
    public void RunListFirstAndLast()
    {
        _ = _list!.First();
        _ = _list!.Last();
    }
}
