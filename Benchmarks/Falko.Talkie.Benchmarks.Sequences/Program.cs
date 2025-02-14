using BenchmarkDotNet.Running;
using Talkie.Benchmarks.Benchmarks;

BenchmarkRunner.Run<AssetVsNativeArgumentExceptionBenchmark>();
BenchmarkRunner.Run<FieldVsPropertyInstanceGettingBenchmark>();
BenchmarkRunner.Run<FieldVsPropertyStaticGettingBenchmark>();
BenchmarkRunner.Run<FrozenSequenceForEachAsyncBenchmark>();
BenchmarkRunner.Run<FrozenSequenceForEachBenchmark>();
BenchmarkRunner.Run<FrozenSequenceVsListGettingByIndexBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherFirstAndLastBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherAddBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherRemoveBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherForEachBenchmark>();
