using System.Text.RegularExpressions;
using CoNET.Utils.Text;

namespace CoNET.Test.Lib;

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
    [InlineData("ext1.ext2.ext3.ext4.ext5", ".", 0, "ext1")]
    [InlineData("ext1.ext2.ext3.ext4.ext5", ".", 1, "ext2")]
    [InlineData("ext1.ext2.ext3.ext4.ext5", ".", 2, "ext3")]
    [InlineData("ext1.ext2.ext3.ext4.ext5", ".", 3, "ext4")]
    [InlineData("ext1.ext2.ext3.ext4.ext5", ".", 4, "ext5")]
    public static void GetSegment(string @string, string separator, int index, string result)
    {
        Assert.Equal(@string.GetSegment(separator, index).ToString(), result);
    }

    [Theory]
    [InlineData("ext1.ext2.ext3", ".", "ext1")]
    public static void GetFirstSegment(string @string, string separator, string result)
    {
        Assert.Equal(@string.GetFirstSegment(separator).ToString(), result);
    }

    [Theory]
    [InlineData("ext1.ext2.ext3", ".", "ext3")]
    public static void GetLastSegment(string @string, string separator, string result)
    {
        Assert.Equal(@string.GetLastSegment(separator).ToString(), result);
    }

    [Fact]
    public static void ToDictionary_Empty()
    {
        var @string = "";
        var dictionary = @string.ToDictionary(";", "=");
        Assert.Equal(dictionary, new Dictionary<string, string> { });
    }

    [Fact]
    public static void ToDictionary_Single()
    {
        var @string = "KeyA=ValueA";
        var dictionary = @string.ToDictionary(";", "=");
        Assert.Equal(dictionary, new Dictionary<string, string> {
            {"KeyA", "ValueA"}
        });
    }

    [Fact]
    public static void ToDictionary_Multple()
    {
        var @string = "KeyA=ValueA;KeyB=ValueB;KeyC=ValueC";
        var dictionary = @string.ToDictionary(";", "=");
        Assert.Equal(dictionary, new Dictionary<string, string> {
            {"KeyA", "ValueA"},
            {"KeyB", "ValueB"},
            {"KeyC", "ValueC"},
        });
    }

}