using System;
using System.IO;
using System.Security.Cryptography;

namespace FileProcessingApp;
public static class Utilities
{
    public static string ComputeFileHash(string filePath)
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
