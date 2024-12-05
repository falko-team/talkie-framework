using BenchmarkDotNet.Running;
using Talkie.Benchmarks.Benchmarks;

BenchmarkRunner.Run<FrozenSequenceForEachAsyncBenchmark>();
BenchmarkRunner.Run<FrozenSequenceForEachBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherFirstAndLastBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherAddBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherRemoveBenchmark>();
BenchmarkRunner.Run<SequencesVsOtherForEachBenchmark>();
