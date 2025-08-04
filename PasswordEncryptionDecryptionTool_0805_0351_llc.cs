// 代码生成时间: 2025-08-05 03:51:01
using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordTool
{
    public class PasswordEncryptionDecryptionTool
    {
        private readonly string _encryptionKey;

        public PasswordEncryptionDecryptionTool(string encryptionKey)
        {
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentException("Encryption key cannot be null or empty.", nameof(encryptionKey));
            }

            _encryptionKey = encryptionKey;
        }

        /// <summary>
        /// Encrypts the provided password using the AES algorithm.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The encrypted password as a base64 string.</returns>
        public string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                    }

                    byte[] iv = aesAlg.IV;
                    byte[] encryptedPassword = msEncrypt.ToArray();

                    using (var msIv = new MemoryStream())
                    {
                        msIv.Write(iv, 0, iv.Length);
                        msIv.Write(encryptedPassword, 0, encryptedPassword.Length);
                        byte[] combined = msIv.ToArray();

                        return Convert.ToBase64String(combined);
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the provided encrypted password.
        /// </summary>
        /// <param name="encryptedPassword">The encrypted password to decrypt.</param>
        /// <returns>The decrypted password as a string.</returns>
        public string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
            {
                throw new ArgumentException("Encrypted password cannot be null or empty.", nameof(encryptedPassword));
            }

            byte[] combined = Convert.FromBase64String(encryptedPassword);
            using (var msIv = new MemoryStream(combined.Length))
            {
                byte[] iv = new byte[Aes.BlockSize];
                byte[] encryptedPasswordBytes = new byte[combined.Length - Aes.BlockSize];

                msIv.Read(iv, 0, Aes.BlockSize);
                msIv.Read(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                    aesAlg.IV = iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var msDecrypt = new MemoryStream(encryptedPasswordBytes))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
