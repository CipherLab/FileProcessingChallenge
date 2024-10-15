//Updated ProcessLinesBenchmark.cs
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using Bogus;
using FileProcessingApp;
using System.Threading.Tasks;

namespace FileProcessingBenchmark
{
    [MemoryDiagnoser(false)]
    public class CPUBoundProcessingLinesBenchmarks
    {
        private ILineProcessor OptimizedCPUBoundProcessor = new OptimizedCPUBoundProcessor();
        private ILineProcessor CPUBoundProcessor = new CPUBoundLineProcessor();


        [Params(100000)] // Parameterize the number of lines
        public int NumLines { get; set; }

        private string[] lines;

        [GlobalSetup]
        public void Setup()
        {
            var faker = new Faker();
            lines = Enumerable.Range(0, NumLines)
                             .Select(i => faker.Lorem.Sentence())
                             .ToArray();
        }

        [Benchmark(Baseline = true)]
        public async Task ProcessLines_Asynchronous_IOBound()
        {
            var processedLines = await CPUBoundProcessor.ProcessLinesAsync(lines);
        }

        [Benchmark]
        public async Task ProcessLines_Asynchronous_IOBound_Optimized()
        {
            var processedLines = await OptimizedCPUBoundProcessor.ProcessLinesAsync(lines);
        }


    }
}