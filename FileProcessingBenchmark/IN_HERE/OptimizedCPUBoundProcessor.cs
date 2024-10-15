using FileProcessingApp;
using System.Security.Cryptography;
using System.Text;

public class OptimizedCPUBoundProcessor : ILineProcessor
{
    public async Task<string> ProcessLineAsync(string line)
    {
        // Simulate an asynchronous operation
        return await Task.Run(() => ProcessLine(line));
    }

    public string ProcessLine(string line)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(line);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public async Task<IEnumerable<string>> ProcessLinesAsync(string[] lines)
    {

        string[] results = new string[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            results[i] = await this.ProcessLineAsync(lines[i]);
        }
        return results;
    }

    public IEnumerable<string> ProcessLines(string[] lines)
    {
        string[] results = new string[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            results[i] = ProcessLine(lines[i]);
        }
        return results;
    }
}
