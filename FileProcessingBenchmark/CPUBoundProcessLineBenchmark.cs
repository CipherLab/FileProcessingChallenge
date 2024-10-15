using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using FileProcessingApp;

namespace FileProcessingBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(iterationCount: 10)] // Run each benchmark 10 times
    public class CPUBoundProcessingLineBenchmarks
    {
        private string sampleLine;

        private ILineProcessor CPUBoundLineProcessor = new CPUBoundLineProcessor();
        private ILineProcessor OptimizedCPUBoundProcessor = new OptimizedCPUBoundProcessor();
        [GlobalSetup]
        public void Setup()
        {
            sampleLine = "This is a sample line for benchmarking purposes.";

        }


        [Benchmark(Baseline = true)]
        public void ProcessLine_Synchronous()
        {
            var processedLine = CPUBoundLineProcessor.ProcessLine(sampleLine);
        }

        [Benchmark]
        public void ProcessLine_Optimized()
        {
            var processedLine = OptimizedCPUBoundProcessor.ProcessLine(sampleLine);
        }




    }
}
