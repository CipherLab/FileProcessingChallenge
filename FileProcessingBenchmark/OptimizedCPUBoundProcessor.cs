using FileProcessingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class OptimizedCPUBoundProcessor : ILineProcessor
{
    public async Task<string> ProcessLineAsync(string line)
    {
        // The operation is CPU-bound and completes synchronously
        string result = ProcessLine(line);
        return await Task.FromResult(result);
    }

    public string ProcessLine(string line)
    {
        ReadOnlySpan<char> chars = line.AsSpan();
        int maxByteCount = Encoding.UTF8.GetMaxByteCount(chars.Length);

        // Use stackalloc for small sizes to prevent stack overflow
        Span<byte> bytes = maxByteCount <= 256 ? stackalloc byte[maxByteCount] : new byte[maxByteCount];
        int bytesEncoded = Encoding.UTF8.GetBytes(chars, bytes);

        Span<byte> hash = stackalloc byte[32]; // SHA256 hash size is 32 bytes

        // Use the static method to compute the hash without creating an instance
        if (!SHA256.TryHashData(bytes.Slice(0, bytesEncoded), hash, out int _))
        {
            throw new InvalidOperationException("Hash computation failed.");
        }

        Span<char> base64Chars = stackalloc char[44]; // Base64-encoded SHA256 hash length is 44 characters
        if (!Convert.TryToBase64Chars(hash, base64Chars, out int charsWritten))
        {
            throw new InvalidOperationException("Base64 encoding failed.");
        }

        return new string(base64Chars.Slice(0, charsWritten));
    }

    public async Task<IEnumerable<string>> ProcessLinesAsync(string[] lines)
    {
        var processedLines = new string[lines.Length];

        // Use Parallel.ForEachAsync for parallel processing
        await Parallel.ForEachAsync(Enumerable.Range(0, lines.Length), async (i, _) =>
        {
            processedLines[i] = ProcessLine(lines[i]);
            await Task.CompletedTask;
        });

        return processedLines;
    }

    public IEnumerable<string> ProcessLines(string[] lines)
    {
        var processedLines = new string[lines.Length];

        // Use Parallel.For for synchronous parallel processing
        Parallel.For(0, lines.Length, i =>
        {
            processedLines[i] = ProcessLine(lines[i]);
        });

        return processedLines;
    }
}
