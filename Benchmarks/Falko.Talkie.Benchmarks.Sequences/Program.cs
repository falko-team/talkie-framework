using BenchmarkDotNet.Running;
using Falko.Talkie.Benchmarks.Benchmarks;

BenchmarkRunner.Run<AddBenchmark>();
BenchmarkRunner.Run<RemoveBenchmark>();
BenchmarkRunner.Run<ForeachBenchmark>();
