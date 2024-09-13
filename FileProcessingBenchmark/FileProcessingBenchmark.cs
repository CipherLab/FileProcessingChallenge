using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using FileProcessingApp;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FileProcessingBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(iterationCount: 10)] // Run each benchmark 10 times
    public class ProcessingBenchmarks
    {
        private string[] lines;

        [GlobalSetup]
        public void Setup()
        {
            lines = File.ReadAllLines("input.txt");
        }

        [Benchmark(Baseline = true)]
        public void ProcessLines_Synchronous()
        {
            var processedLines = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                processedLines[i] = Processor.ProcessLine(lines[i]);
            }
        }

        [Benchmark]
        public void ProcessLines_Parallel()
        {
            var processedLines = new string[lines.Length];

            System.Threading.Tasks.Parallel.For(0, lines.Length, i =>
            {
                processedLines[i] = Processor.ProcessLine(lines[i]);
            });
        }

        [Benchmark]
        public async Task ProcessLines_Asynchronous()
        {
            var processedLines = new string[lines.Length];

            var tasks = new Task<string>[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                tasks[i] = Processor.ProcessLineAsync(lines[i]);
            }

            var results = await Task.WhenAll(tasks);

            for (int i = 0; i < results.Length; i++)
            {
                processedLines[i] = results[i];
            }
        }
    }
}
