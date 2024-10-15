// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using FileProcessingBenchmark;

Console.WriteLine("Hello, World!");
//var cpuProcessLineSummary = BenchmarkRunner.Run<CPUBoundProcessingLineBenchmarks>();
//var ioProcessLineSummary = BenchmarkRunner.Run<IOBoundProcessingLineBenchmarks>();

//var cpuProcessLinesSummary = BenchmarkRunner.Run<CPUBoundProcessingLinesBenchmarks>();
var ioProcessLinesSummary = BenchmarkRunner.Run<IOBoundProcessingLinesBenchmarks>();
