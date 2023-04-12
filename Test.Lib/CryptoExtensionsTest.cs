using System.Text;
using CoNET.Utils.Crypto;

namespace CoNET.Test.Lib;

public static class CryptoExtensionsTest
{

    public const string Message = "A secret message or data to protect";

    [Fact]
    public static void GenerateKeyPairTest()
    {
        var data = new byte[0];
        var kPair = data.GenerateKeyPair();
        Assert.NotNull(kPair.privKey);
        Assert.NotNull(kPair.pubKey);
    }

    [Theory]
    [InlineData(Message)]
    public static void EncryptDecryptTest(string message)
    {
        var data = Encoding.UTF8.GetBytes(message);
        var kPair = data.GenerateKeyPair();
        var encrypted = data.Encrypt(kPair.pubKey);
        var decrypted = encrypted.Decrypt(kPair.privKey);
        var payload = Encoding.UTF8.GetString(decrypted);
        Assert.Equal(message, payload);
    }

    [Theory]
    [InlineData(Message)]
    public static void SignVerifyData(string message)
    {
        var data = Encoding.UTF8.GetBytes(message);
        var kPair = data.GenerateKeyPair();
        var signature = data.SignData(kPair.privKey);
        var verification = data.VerifyData(signature, kPair.pubKey);
        Assert.True(verification);
    }

    [Theory]
    [InlineData(Message)]
    public static void SignVerifyHash(string message)
    {
        var data = Encoding.UTF8.GetBytes(message);
        var hash = data.ComputeHash();
        var kPair = data.GenerateKeyPair();
        var signature = hash.SignHash(kPair.privKey);
        var verification = hash.VerifyHash(signature, kPair.pubKey);
        Assert.True(verification);
    }

}