using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using Bogus;
using FileProcessingApp;
using System.Threading.Tasks;

namespace FileProcessingBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(iterationCount: 10)] // Run each benchmark 10 times
    public class ProcessingLinesBenchmarks
    {
        private ILineProcessor BaseProcessor = new LineProcessor();
        private ILineProcessor OptimizedProcessor = new OptimizedLineProcessor();

        private string[] lines;
        private string largeFilePath = "largeInput.txt";

        [GlobalSetup]
        public void Setup()
        {
            // Generate mock data using Bogus
            var faker = new Faker();
            lines = new string[100000]; // Generate 100,000 lines of mock data
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = faker.Lorem.Sentence();
            }

            // Write the mock data to a large file
            File.WriteAllLines(largeFilePath, lines);
        }

        [Benchmark(Baseline = true)]
        public async Task ProcessLines_Asynchronous()
        {
            var processedLines = await BaseProcessor.ProcessLinesAsync(lines);
        }

        [Benchmark]
        public async Task ProcessLines_Asynchronous_Optimized()
        {
            var processedLines = await OptimizedProcessor.ProcessLinesAsync(lines);
        }
    }
}
