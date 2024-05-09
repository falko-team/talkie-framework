using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Falko.Unibot.Collections;
using Falko.Unibot.Models;

namespace Falko.Unibot.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RunStrategy.Throughput)]
public class AddBenchmark
{
    [Params(0, 1, 10, 100, 1000, 10000)]
    public int Capacity { get; set; }

    [Benchmark]
    public void RunRemovableSequenceAdd()
    {
        var sequence = new RemovableSequence<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            _ = sequence.Add(Reference.Shared);
        }

        _ = sequence;
    }

    [Benchmark]
    public void RunSequenceAdd()
    {
        var sequence = new Sequence<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            sequence.Add(Reference.Shared);
        }

        _ = sequence;
    }

    [Benchmark]
    public void RunLinkedListAddLast()
    {
        var linkedList = new LinkedList<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            _ = linkedList.AddLast(Reference.Shared);
        }

        _ = linkedList;
    }

    [Benchmark]
    public void RunLinkedListAddFirst()
    {
        var linkedList = new LinkedList<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            _ = linkedList.AddFirst(Reference.Shared);
        }

        _ = linkedList;
    }

    [Benchmark]
    public void RunListAdd()
    {
        var list = new List<Reference>();

        for (var i = 0; i < Capacity; i++)
        {
            list.Add(Reference.Shared);
        }

        _ = list;
    }
}
