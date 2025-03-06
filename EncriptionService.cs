using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionService
{
    private byte[] key;

    public EncryptionService(string passphrase)
    {
        using (var sha256 = SHA256.Create())
        {
            key = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
        }
    }

    public string Encrypt(string plainText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.GenerateIV();
            var iv = aes.IV;

            using (var msEncrypt = new MemoryStream())
            {
                
                msEncrypt.Write(iv, 0, iv.Length);

                using (var cryptoStream = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    var plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        var combinedBytes = Convert.FromBase64String(cipherText);

        if (combinedBytes.Length < 16)
        {
            throw new ArgumentException("Invalid cipher text length.");
        }

        using (var aes = Aes.Create())
        {
            aes.Key = key;


            var iv = new byte[16];
            Array.Copy(combinedBytes, 0, iv, 0, iv.Length);

           
            var encryptedMessage = new byte[combinedBytes.Length - iv.Length];
            Array.Copy(combinedBytes, iv.Length, encryptedMessage, 0, encryptedMessage.Length);

            using (var msDecrypt = new MemoryStream(encryptedMessage))
            using (var cryptoStream = new CryptoStream(msDecrypt, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
