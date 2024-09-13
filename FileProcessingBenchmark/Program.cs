// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using FileProcessingBenchmark;

Console.WriteLine("Hello, World!");
//var processLineSummary = BenchmarkRunner.Run<ProcessingLineBenchmarks>();
var processLinesSummary = BenchmarkRunner.Run<ProcessingLinesBenchmarks>();
