using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Sloth.Shared.Helpers;
public static class EncryptionHelper
{
    // Ensure these are exactly 32 bytes and 16 bytes respectively
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); // 32 bytes
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes

    /// <summary>
    /// Encrypts an object of type T and returns the encrypted string.
    /// </summary>
    public static string EncryptObject<T>(T obj)
    {
        // Serialize the object to JSON
        string json = JsonConvert.SerializeObject(obj);
        return EncryptString(json);
    }

    /// <summary>
    /// Decrypts an encrypted string and returns the object of type T.
    /// </summary>
    public static T? DecryptObject<T>(string encryptedString) where T : class, new()
    {
        // Decrypt the string
        string json = DecryptString(encryptedString);
        return JsonHelper.TryConvert(json, new T());
    }

    private static string EncryptString(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    private static string DecryptString(string cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}
