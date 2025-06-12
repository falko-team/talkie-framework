using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Falko.Talkie.Benchmarks.Benchmarks;

[SimpleJob(RunStrategy.Throughput)]
public class FieldVsPropertyInstanceGettingBenchmark
{
    private const int IterationsCount = 1000;

    private readonly Property _property = new();

    private readonly Field _field = new();

    [Benchmark]
    public void PropertyGetting()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            _ = _property.Number;
        }
    }

    [Benchmark]
    public void FieldGetting()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            _ = _field.Number;
        }
    }

    private readonly struct Property(int number = 1)
    {
        public int Number => number;
    }

    private readonly struct Field(int number = 1)
    {
        public readonly int Number = number;
    }
}
