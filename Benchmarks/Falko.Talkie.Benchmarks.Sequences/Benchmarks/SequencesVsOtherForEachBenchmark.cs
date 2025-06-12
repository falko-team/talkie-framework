using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Falko.Talkie.Sequences;
using Talkie.Models;

namespace Falko.Talkie.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Throughput)]
public class SequencesVsOtherForEachBenchmark
{
    private FrozenSequence<Reference>? _frozenSequence;

    private IEnumerable<Reference>? _referenceFrozenSequence;

    private RemovableSequence<Reference>? _removableSequence;

    private Sequence<Reference>? _sequence;

    private LinkedList<Reference>? _linkedList;

    private List<Reference>? _list;

    private IEnumerable<Reference>? _referenceList;

    [Params(0, 1, 10, 100, 1000, 10000)]
    public int Capacity { get; set; }

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
        _referenceList = _list;
    }

    [Benchmark]
    public void RunFrozenSequenceForeach()
    {
        foreach (var value in _frozenSequence!)
        {
            _ = value;
        }
    }

    [Benchmark]
    public void RunReferenceFrozenSequenceForeach()
    {
        foreach (var value in _referenceFrozenSequence!)
        {
            _ = value;
        }
    }

    [Benchmark]
    public void RunRemovableSequenceForeach()
    {
        foreach (var value in _removableSequence!)
        {
            _ = value;
        }
    }

    [Benchmark]
    public void RunSequenceForeach()
    {
        foreach (var value in _sequence!)
        {
            _ = value;
        }
    }

    [Benchmark]
    public void RunLinkedListForeach()
    {
        foreach (var value in _linkedList!)
        {
            _ = value;
        }
    }

    [Benchmark]
    public void RunListForeach()
    {
        foreach (var value in _list!)
        {
            _ = value;
        }
    }

    [Benchmark]
    public void RunReferenceListForeach()
    {
        foreach (var value in _referenceList!)
        {
            _ = value;
        }
    }
}
