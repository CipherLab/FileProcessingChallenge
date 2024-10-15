using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using FileProcessingApp;

namespace FileProcessingBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(iterationCount: 10)] // Run each benchmark 10 times
    public class IOBoundProcessingLineBenchmarks
    {
        private string sampleLine;

        private ILineProcessor IOBoundProcessor = new IOBoundLineProcessor();
        private ILineProcessor OptimizedIOBoundProcessor = new OptimizedIOBoundLineProcessor();
        [GlobalSetup]
        public void Setup()
        {
            sampleLine = "This is a sample line for benchmarking purposes.";

        }


        [Benchmark(Baseline = true)]
        public void ProcessLine_Synchronous()
        {
            var processedLine = IOBoundProcessor.ProcessLine(sampleLine);
        }

        [Benchmark]
        public void ProcessLine_Optimized()
        {
            var processedLine = OptimizedIOBoundProcessor.ProcessLine(sampleLine);
        }



    }
}
