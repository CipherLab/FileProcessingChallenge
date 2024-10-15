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
    public class IOBoundProcessingLinesBenchmarks
    {
        private ILineProcessor IOBoundProcessor = new IOBoundLineProcessor();
        private ILineProcessor OptimizedIOBoundProcessor = new OptimizedIOBoundLineProcessor();


        [Params(10000)] // Parameterize the number of lines
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

        public async Task ProcessLines_Asynchronous()
        {
            var processedLines = await IOBoundProcessor.ProcessLinesAsync(lines);
        }

        [Benchmark]
        public async Task ProcessLines_Asynchronous_Optimized()
        {
            var processedLines = await OptimizedIOBoundProcessor.ProcessLinesAsync(lines);
        }


    }
}