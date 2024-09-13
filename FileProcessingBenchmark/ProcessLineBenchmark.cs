using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using Bogus;
using FileProcessingApp;

namespace FileProcessingBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(iterationCount: 10)] // Run each benchmark 10 times
    public class ProcessingLineBenchmarks
    {
        private string sampleLine;

        private ILineProcessor BaseProcessor = new LineProcessor();
        private ILineProcessor OptimizedProcessor = new OptimizedLineProcessor();
        [GlobalSetup]
        public void Setup()
        {
            sampleLine = "This is a sample line for benchmarking purposes.";
        
        }


        [Benchmark(Baseline = true)]
        public void ProcessLine_Synchronous()
        {
            var processedLine = BaseProcessor.ProcessLine(sampleLine);
        }

        [Benchmark]
        public void ProcessLine_Optimized()
        {
            var processedLine = OptimizedProcessor.ProcessLine(sampleLine);
        }

    }
}
