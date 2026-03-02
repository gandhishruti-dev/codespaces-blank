using System;
using System.Text.RegularExpressions;

namespace MyWebApi.Logging
{
    public static class PiiProtectedLogger
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

        public static string MaskPii(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string masked = input;

            foreach (var pattern in PiiPatterns)
            {
                masked = Regex.Replace(masked, pattern, "[REDACTED]");
            }

            return masked;
        }

        public static void LogInfo(string message)
        {
            var maskedMessage = MaskPii(message);
            Console.WriteLine($"[INFO] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {maskedMessage}");
        }

        public static void LogError(string message, Exception ex = null)
        {
            var maskedMessage = MaskPii(message);
            var maskedExceptionMessage = ex != null ? MaskPii(ex.Message) : string.Empty;
            
            Console.WriteLine($"[ERROR] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {maskedMessage}");
            if (ex != null)
                Console.WriteLine($"[ERROR] Exception: {maskedExceptionMessage}");
        }

        public static void LogWarning(string message)
        {
            var maskedMessage = MaskPii(message);
            Console.WriteLine($"[WARNING] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {maskedMessage}");
        }
    }
}