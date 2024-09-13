using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessingApp;
public static class Processor
{
    public static async Task<string> ProcessLineAsync(string line)
    {
        // Simulate a costly operation asynchronously
        return await Task.Run(() => ProcessLine(line));
    }

    public static string ProcessLine(string line)
    {
        // Simulate a costly operation (e.g., hashing)
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(line);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
