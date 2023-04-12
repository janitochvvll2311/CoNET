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

    public static string GetSegment(this string @string, string separator, int index)
    {
        var segments = @string.Split(separator);
        var segment = segments[index];
        return segment;
    }

    public static string GetFirstSegment(this string @string, string separator)
    {
        var index = @string.IndexOf(separator);
        var segment = @string.Substring(0, index);
        return segment;
    }

    public static string GetLastSegment(this string @string, string separator)
    {
        var index = @string.LastIndexOf(separator);
        var segment = @string.Substring(index + 1);
        return segment;
    }

}