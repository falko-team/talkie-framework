using BenchmarkDotNet.Running;
using Falko.Unibot.Benchmarks;

BenchmarkRunner.Run<AddBenchmark>();
BenchmarkRunner.Run<RemoveBenchmark>();
BenchmarkRunner.Run<ForeachBenchmark>();
