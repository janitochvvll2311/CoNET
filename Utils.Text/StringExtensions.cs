using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

    public static ReadOnlySpan<char> GetSegment(this ReadOnlySpan<char> @string, string separator, int index)
    {
        var current = @string;
        for (var i = 0; i < index; i++)
        {
            current = current.Slice(current.IndexOf(separator) + 1);
        }
        var length = current.IndexOf(separator);
        if (length > 0)
        {
            current = current.Slice(0, length);
        }
        return current;
    }

    public static ReadOnlySpan<char> GetSegment(this string @string, string separator, int index)
        => @string.AsSpan().GetSegment(separator, index);

    public static ReadOnlySpan<char> GetFirstSegment(this ReadOnlySpan<char> @string, string separator)
    {
        var index = @string.IndexOf(separator);
        var segment = @string.Slice(0, index);
        return segment;
    }

    public static ReadOnlySpan<char> GetFirstSegment(this string @string, string separator)
        => @string.AsSpan().GetFirstSegment(separator);

    public static ReadOnlySpan<char> GetLastSegment(this ReadOnlySpan<char> @string, string separator)
    {
        var index = @string.LastIndexOf(separator);
        var segment = @string.Slice(index + 1);
        return segment;
    }

    public static ReadOnlySpan<char> GetLastSegment(this string @string, string separator)
        => @string.AsSpan().GetLastSegment(separator);

    public static Dictionary<string, string> ToDictionary(this ReadOnlySpan<char> @string, string pairSeparator, string keyValueSeparator)
    {
        var dictionary = new Dictionary<string, string>();
        var current = @string;
        var offset = 0;
        while ((offset = current.IndexOf(pairSeparator)) > 0)
        {
            var pair = current.Slice(0, offset);
            dictionary[pair.GetFirstSegment(keyValueSeparator).ToString()] = pair.GetLastSegment(keyValueSeparator).ToString();
            current = current.Slice(offset + 1);
        }
        offset = current.IndexOf(keyValueSeparator);
        if (offset > 0)
        {
            dictionary[current.GetFirstSegment(keyValueSeparator).ToString()] = current.GetLastSegment(keyValueSeparator).ToString();
        }
        return dictionary;
    }

    public static Dictionary<string, string> ToDictionary(this string @string, string pairSeparator, string keyValueSeparator)
        => @string.AsSpan().ToDictionary(pairSeparator, keyValueSeparator);

}