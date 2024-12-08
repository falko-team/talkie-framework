using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Talkie.Models;
using Talkie.Validation;

namespace Talkie.Benchmarks.Benchmarks;

[SimpleJob(RunStrategy.Throughput)]
public class AssetVsNativeArgumentExceptionBenchmark
{
    private const int IterationsCount = 100;

    [Benchmark]
    public void NativeCheck()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            ArgumentNullException.ThrowIfNull(Reference.Shared);
        }
    }

    [Benchmark]
    public void AssetCheck()
    {
        for (var i = 0; i < IterationsCount; i++)
        {
            Assert.ArgumentNullException.ThrowIfNull(Reference.Shared);
        }
    }
}
