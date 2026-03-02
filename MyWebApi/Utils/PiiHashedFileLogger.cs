using System;
using System.Text.RegularExpressions;
using MyWebApi.Interfaces;

namespace MyWebApi.Logging
{
    public class PiiHashedFileLogger: ILoggerService
    {
        private static readonly string[] PiiPatterns = new[]
        {
            // Email addresses
            @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b",
            // Social Security Numbers (XXX-XX-XXXX)
            @"\b\d{3}-\d{2}-\d{4}\b",
            // Phone numbers (various formats)
            @"\b(?:\+?1[-.\s]?)?\(?[0-9]{3}\)?[-.\s]?[0-9]{3}[-.\s]?[0-9]{4}\b",
            // Credit card numbers
            @"\b\d{4}[\s-]?\d{4}[\s-]?\d{4}[\s-]?\d{4}\b",
            // ZIP codes
            @"\b\d{5}(?:-\d{4})?\b"
        };

        public static string HashPii(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string hashed = input;

            foreach (var pattern in PiiPatterns)
            {
                hashed = Regex.Replace(hashed, pattern, match => 
                    Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(match.Value)));
            }

            return hashed;
        }
        public void LogInfo(string message)
        {
            var hashedMessage = HashPii(message);
            // Code to write hashedMessage to a file
            System.IO.File.AppendAllText("Data/logs.txt", $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {hashedMessage}\n");
        }   

        public void LogError(string message, Exception ex = null)
        {
            var hashedMessage = HashPii(message);
            var hashedExceptionMessage = ex != null ? HashPii(ex.Message) : string.Empty;
            // Code to write hashedMessage and hashedExceptionMessage to a file
            System.IO.File.AppendAllText("Data/logs.txt", $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - ERROR: {hashedMessage}\n");
            if (ex != null)
                System.IO.File.AppendAllText("Data/logs.txt", $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - ERROR EXCEPTION: {hashedExceptionMessage}\n");
        }

        public void LogWarning(string message)
        {
            var hashedMessage = HashPii(message);
            // Code to write hashedMessage to a file
            System.IO.File.AppendAllText("Data/logs.txt", $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - WARNING: {hashedMessage}\n");
        }
    }
}
