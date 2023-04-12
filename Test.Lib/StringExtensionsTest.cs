using System.Text.RegularExpressions;
using CoNET.Utils.Text;

namespace Test.Lib;

public static class StringExtensionsTest
{

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("    ", true)]
    [InlineData("\t\n", true)]
    [InlineData("  0  ", false)]
    public static void IsNullOrBlank(string? @string, bool result)
    {
        Assert.Equal(@string.IsNullOrBlank(), result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("    ", false)]
    [InlineData("\t\n", false)]
    [InlineData("  0  ", true)]
    public static void IsNotNullOrBlank(string? @string, bool result)
    {
        Assert.Equal(@string.IsNotNullOrBlank(), result);
    }

    [Theory]
    [InlineData("ext1.ext2.ext3", ".", 1, "ext2")]
    public static void GetSegment(string @string, string separator, int index, string result)
    {
        Assert.Equal(@string.GetSegment(separator, index), result);
    }

    [Theory]
    [InlineData("ext1.ext2.ext3", ".", "ext1")]
    public static void GetFirstSegment(string @string, string separator, string result)
    {
        Assert.Equal(@string.GetFirstSegment(separator), result);
    }

    [Theory]
    [InlineData("ext1.ext2.ext3", ".", "ext3")]
    public static void GetLastSegment(string @string, string separator, string result)
    {
        Assert.Equal(@string.GetLastSegment(separator), result);
    }

}