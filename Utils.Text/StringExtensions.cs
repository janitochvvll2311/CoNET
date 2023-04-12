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
        var offset = 0;
        for (var i = 0; i < index && offset < @string.Length; i++)
        {
            offset = @string.IndexOf(separator, offset) + separator.Length;
        }
        var length = @string.IndexOf(separator, offset) - offset;
        if (length > 0)
        {
            var segment = @string.Substring(offset, length);
            return segment;
        }
        else
        {
            var segment = @string.Substring(offset);
            return segment;
        }
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