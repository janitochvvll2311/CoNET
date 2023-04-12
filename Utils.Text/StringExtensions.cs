using System;

namespace CoNET.Utils.Text;

public static class StringExtensions
{

    public static bool IsNullOrBlank(this string? @string)
    {
        var result = string.IsNullOrWhiteSpace(@string);
        return result;
    }

    public static bool IsNotNullOrBlank(this string? @string)
    {
        var result = !string.IsNullOrWhiteSpace(@string);
        return result;
    }

}