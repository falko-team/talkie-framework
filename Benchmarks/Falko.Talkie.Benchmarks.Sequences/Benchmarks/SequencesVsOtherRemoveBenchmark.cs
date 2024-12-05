using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Talkie.Models;
using Talkie.Sequences;

namespace Talkie.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Throughput)]
public class SequencesVsOtherRemoveBenchmark
{
    private IEnumerable<RemovableSequence<Reference>.Remover>? _removableSequenceRemovers;

    private LinkedList<Reference>? _linkedList;

    private IEnumerable<Reference>? _linkedListValues;

    private List<Reference>? _list;

    private IEnumerable<Reference>? _listValues;

    [Params(0, 1, 10, 100, 1000, 10000)]
    public int Capacity { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        SetupRemovableSequenceRemove();
        SetupLinkedListRemove();
        SetupListRemove();
    }

    public void SetupRemovableSequenceRemove()
    {
        var removersSequence = new Sequence<RemovableSequence<Reference>.Remover>();

        var removableSequence = new RemovableSequence<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            removersSequence.Add(removableSequence.Add(new Reference()));
        }

        _removableSequenceRemovers = removersSequence.Reverse().ToArray();
    }

    public void SetupLinkedListRemove()
    {
        var linkedListValues = new Sequence<Reference>();

        var linkedList = new LinkedList<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            var reference = new Reference();

            linkedList.AddLast(reference);
            linkedListValues.Add(reference);
        }

        _linkedList = linkedList;
        _linkedListValues = linkedListValues.Reverse().ToArray();
    }

    public void SetupListRemove()
    {
        var listValues = new Sequence<Reference>();

        var list = new List<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            var reference = new Reference();

            list.Add(reference);
            listValues.Add(reference);
        }

        _list = list;
        _listValues = listValues.Reverse().ToArray();
    }

    [Benchmark]
    public void RunRemovableSequenceRemove()
    {
        foreach (var value in _removableSequenceRemovers!)
        {
            value.Remove();
        }
    }

    [Benchmark]
    public void RunLinkedListRemove()
    {
        foreach (var value in _linkedListValues!)
        {
            _linkedList!.Remove(value);
        }
    }

    [Benchmark]
    public void RunListRemove()
    {
        foreach (var value in _listValues!)
        {
            _list!.Remove(value);
        }
    }
}
