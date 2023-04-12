using System.Text.RegularExpressions;
using CoNET.Utils.Text;

namespace Test.Lib;

public static class StringExtensionsTest
{

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("\t\n")]
    public static void IsNullOrBlank_True(string? @string)
    {
        Assert.True(@string.IsNullOrBlank());
    }

    [Theory]
    [InlineData("  0  ")]
    public static void IsNullOrBlank_False(string? @string)
    {
        Assert.False(@string.IsNullOrBlank());
    }

    [Theory]
    [InlineData("  0  ")]
    public static void IsNotNullOrBlank_True(string? @string)
    {
        Assert.True(@string.IsNotNullOrBlank());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("\t\n")]
    public static void IsNotNullOrBlank_False(string? @string)
    {
        Assert.False(@string.IsNotNullOrBlank());
    }

}