using BenchmarkDotNet.Running;
using Talkie.Benchmarks.Benchmarks;

BenchmarkRunner.Run<AddBenchmark>();
BenchmarkRunner.Run<FirstAndLastBenchmark>();
BenchmarkRunner.Run<RemoveBenchmark>();
BenchmarkRunner.Run<ForeachBenchmark>();
