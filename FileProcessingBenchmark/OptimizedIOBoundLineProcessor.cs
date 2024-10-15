using FileProcessingApp;
using System.Security.Cryptography;
using System.Text;

public class OptimizedIOBoundLineProcessor : ILineProcessor
{
    public async Task<string> ProcessLineAsync(string line)
    {
        using (MemoryStream stream = new MemoryStream())
        using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8, 4096, leaveOpen: true))
        {
            await writer.WriteAsync(line);
            await writer.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 4096, true))
            {
                string content = await reader.ReadToEndAsync();
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(content);
                    byte[] hash = sha256.ComputeHash(bytes);
                    return Convert.ToBase64String(hash);
                }
            }
        }
    }

    public string ProcessLine(string line) => ProcessLineAsync(line).Result;

    public async Task<IEnumerable<string>> ProcessLinesAsync(string[] lines) => await Task.WhenAll(lines.Select(ProcessLineAsync));

    public IEnumerable<string> ProcessLines(string[] lines) => ProcessLinesAsync(lines).Result;
}