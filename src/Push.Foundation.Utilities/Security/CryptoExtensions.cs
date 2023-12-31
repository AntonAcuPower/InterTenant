﻿using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Push.Foundation.Utilities.Security
{
    public static class CryptoExtensions
    {
        //
        // WARNING *** Do not change (!!!) ***
        //
        // static readonly byte[] PermanentSalt = Encoding.Unicode.GetBytes("Salt This, Baby!");
        //
        private static readonly 
                byte[] PermanentSalt = 
                    Encoding.Unicode.GetBytes("Salt My Boomba, Baby!");
        

        public static string DpApiEncryptString(this SecureString input)
        {
            byte[] encryptedData = 
                ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(input.ToInsecureString()),
                    PermanentSalt,
                    DataProtectionScope.LocalMachine);

            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DpApiDecryptString(this string encryptedData)
        {
            try
            {
                byte[] decryptedData = 
                    ProtectedData.Unprotect(
                        Convert.FromBase64String(encryptedData),
                        PermanentSalt,
                        DataProtectionScope.LocalMachine);

                return Encoding.Unicode.GetString(decryptedData).ToSecureString();
            }
            catch (Exception)
            {
                return new SecureString();
            }
        }

        
        //
        // Warning - due to idiosyncracies, this usage of AES crypto code is particularly volatile
        // ... i.e. not amenable to the slightest change in "using" statement scope changes
        // 
        public static string AesEncryptString(
                    this string input, string keyString, string IVString)
        {
            var Key = Encoding.UTF8.GetBytes(keyString);
            var IV = Encoding.UTF8.GetBytes(IVString);
            byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes(input);

            // Check arguments.
            if (input == null || input.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Key;
                aes.IV = IV;

                byte[] cipherText = null;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                    }

                    cipherText = ms.ToArray();
                }

                // Return the encrypted bytes from the memory stream.
                return Convert.ToBase64String(cipherText);
            }
        }
        
        public static string AesDecryptString(
                    this string encryptedText, string keyString, string IVString)
        {
            var Key = Encoding.UTF8.GetBytes(keyString);
            var IV = Encoding.UTF8.GetBytes(IVString);
            byte[] cipherText = Convert.FromBase64String(encryptedText);
            byte[] plainText = null;

            using (Aes aes = new AesManaged())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Key;
                aes.IV = IV;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherText, 0, cipherText.Length);
                    }

                    plainText = ms.ToArray();
                }

                var s = Encoding.Unicode.GetString(plainText);
                return s;
            }
        }
    }
}
    