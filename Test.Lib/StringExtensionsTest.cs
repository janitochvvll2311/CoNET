using CoNET.Utils.Text;

namespace Test.Lib;

public class StringExtensionsTest
{

    [Fact]
    public void IsNullOrBlank()
    {
        Assert.True((null as string).IsNullOrBlank());
        Assert.True("".IsNullOrBlank());
        Assert.True("    ".IsNullOrBlank());
        Assert.True("\t\n".IsNullOrBlank());
        Assert.False("  0  ".IsNullOrBlank());
    }

    [Fact]
    public void IsNotNullOrBlank()
    {
        Assert.False((null as string).IsNotNullOrBlank());
        Assert.False("".IsNotNullOrBlank());
        Assert.False("    ".IsNotNullOrBlank());
        Assert.False("\t\n".IsNotNullOrBlank());
        Assert.True("  0  ".IsNotNullOrBlank());
    }

}