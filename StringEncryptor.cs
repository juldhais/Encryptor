using System.Security.Cryptography;
using System.Text;

namespace Encryptor;

public static class StringEncryptor
{
    public static string Encrypt(string text, string password)
    {
        // initialize AES and MD5
        using var aes = Aes.Create();
        using var md5 = MD5.Create();

        // generate IV from random string
        var rng = RandomNumberGenerator.Create();
        var ivBytes = new byte[3];
        rng.GetBytes(ivBytes);
        var ivString = Convert.ToBase64String(ivBytes)[..4];
        
        aes.BlockSize = 128;
        aes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(password));
        aes.IV = md5.ComputeHash(Encoding.Unicode.GetBytes(ivString));

        var bytesToEncrypt = Encoding.Unicode.GetBytes(text);
        var encryptor = aes.CreateEncryptor();
        var encryptedBytes = encryptor.TransformFinalBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);
        var encryptedText = Convert.ToBase64String(encryptedBytes);

        return ivString + encryptedText;
    }
    
    public static string Decrypt(string text, string password)
    {
        var iv = text[..4];
        var encryptedText = text[4..];

        using var aes = Aes.Create();
        using var md5 = MD5.Create();

        aes.BlockSize = 128;
        aes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(password));
        aes.IV = md5.ComputeHash(Encoding.Unicode.GetBytes(iv));

        var bytesToDecrypt = Convert.FromBase64String(encryptedText);
        var decryptor = aes.CreateDecryptor();
        var decryptedBytes = decryptor.TransformFinalBlock(bytesToDecrypt, 0, bytesToDecrypt.Length);

        return Encoding.Unicode.GetString(decryptedBytes);
    }
}