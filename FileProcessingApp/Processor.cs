using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace FileProcessingApp;
public class Processor
{
    private ILineProcessor processor;

    public Processor(ILineProcessor processor)
    {
        this.processor = processor;
    }
    public async Task<string> ProcessLineAsync(string line, ILineProcessor processor)
    {
        // Simulate a costly operation asynchronously
        return await Task.Run(() => processor.ProcessLine(line));
    }

    //Baseline (think, original) method for benchmarking
    public  string ProcessLine(string line)
    {
        return processor.ProcessLine(line);
    }



}
