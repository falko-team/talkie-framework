using BenchmarkDotNet.Running;
using Talkie.Benchmarks.Benchmarks;

BenchmarkRunner.Run<AddBenchmark>();
BenchmarkRunner.Run<RemoveBenchmark>();
BenchmarkRunner.Run<ForeachBenchmark>();
