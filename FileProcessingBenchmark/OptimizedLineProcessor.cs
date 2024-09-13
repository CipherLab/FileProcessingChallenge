// See https://aka.ms/new-console-template for more information
using FileProcessingApp;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


// New method for benchmarking
/*
1.	Avoiding Object Creation: 
    The optimized method uses a static SHA256 instance instead of creating a new one for each call. 
    This reduces the overhead of object creation and disposal.
2.	Using Span<T> and stackalloc: 
    The optimized method uses Span<T> and stackalloc to work with memory on the stack instead of the heap. 
    This reduces memory allocations and improves performance by avoiding garbage collection.
3.	Efficient Encoding: 
    The optimized method uses Encoding.UTF8.GetBytes with ReadOnlySpan<char> and Span<byte> to encode the string more efficiently.
4.	Hash Calculation: 
    The optimized method uses SHA256.TryComputeHash to compute the hash directly into a Span<byte>, 
    avoiding additional allocations.
 */


public class OptimizedLineProcessor : ILineProcessor
{
    private static readonly ThreadLocal<SHA256> Sha256 = new ThreadLocal<SHA256>(() => SHA256.Create());

    public Task<string> ProcessLineAsync(string line)
    {
        // Since the operation is CPU-bound and fast, we can return a completed task
        string result = ProcessLine(line);
        return Task.FromResult(result);
    }
    public IEnumerable<string> ProcessLines(string[] lines)
    {

        var processedLines = new List<string>();
        foreach (var line in lines)
        {
            var processedLine = ProcessLine(line);
            processedLines.Add(processedLine);
        }

        return processedLines;
    }

    public string ProcessLine(string line)
    {
        ReadOnlySpan<char> chars = line.AsSpan();
        int maxByteCount = Encoding.UTF8.GetMaxByteCount(chars.Length);
        Span<byte> bytes = stackalloc byte[maxByteCount];
        int bytesEncoded = Encoding.UTF8.GetBytes(chars, bytes);

        Span<byte> hash = stackalloc byte[32]; // SHA256 hash size is 32 bytes
        Sha256.Value!.TryComputeHash(bytes[..bytesEncoded], hash, out int _);

        return Convert.ToBase64String(hash);
    }

    public async Task<IEnumerable<string>> ProcessLinesAsync(string[] lines)
    {

        var processedLines = new List<string>();
        foreach (var line in lines)
        {
            var processedLine = await ProcessLineAsync(line);
            processedLines.Add(processedLine);
        }
        return processedLines;
    }
}
