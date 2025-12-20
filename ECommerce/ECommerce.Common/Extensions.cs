using System;

namespace ECommerce.Common
{
    public static class StringExtensions
    {
        public static string ToHeader(this string text)
        {
            return $"=== {text.ToUpper()} ===";
        }
    }
}