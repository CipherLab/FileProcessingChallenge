using FileProcessingApp;
using System.Security.Cryptography;
using System.Text;

public class OptimizedIOBoundLineProcessor : ILineProcessor
{
    public async Task<string> ProcessLineAsync(string line)
    {
        // Simulate I/O-bound operation: Write to and read from a temporary file.
        string tempFilePath = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFilePath, line);
        string content = await File.ReadAllTextAsync(tempFilePath);
        File.Delete(tempFilePath);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public string ProcessLine(string line) => ProcessLineAsync(line).Result; // Synchronous version

    public async Task<IEnumerable<string>> ProcessLinesAsync(string[] lines)
    {
        List<string> result = new List<string>();
        foreach (string line in lines)
        {
            var retval = await ProcessLineAsync(line);
            result.Add(retval);
        }
        return result;
    }

    public IEnumerable<string> ProcessLines(string[] lines) => ProcessLinesAsync(lines).Result;
}