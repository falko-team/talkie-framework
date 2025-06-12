using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Falko.Talkie.Benchmarks.Benchmarks;

[SimpleJob(RunStrategy.Throughput)]
public class FieldVsPropertyStaticGettingBenchmark
{
    private const int IterationsCount = 1000;

    [Benchmark]
    public void PropertyGetting()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            _ = Property.Number;
        }
    }

    [Benchmark]
    public void FieldGetting()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            _ = Field.Number;
        }
    }

    private readonly struct Property
    {
        public static int Number => 1;
    }

    private readonly struct Field
    {
        public static readonly int Number = 1;
    }
}
