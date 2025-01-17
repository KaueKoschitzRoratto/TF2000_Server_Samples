﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace CustomUserManagement
{
    internal static class HashHelper
    {
        public const int SALT_SIZE = 64;

        public static byte[] GenerateSalt()
        {
            var buffer = new byte[SALT_SIZE];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(buffer, 0, buffer.Length);
            }
            return buffer;
        }

        public static string Sha256(byte[] salt, string input)
        {
            var totalSize = salt.Length + input.Length;
            if (totalSize == 0)
            {
                return string.Empty;
            }
            var bytes = new byte[totalSize];

            if (salt.Length > 0)
            {
                Array.Copy(salt, bytes, salt.Length);
            }
            if (input.Length > 0)
            {
                Array.Copy(Encoding.UTF8.GetBytes(input), 0, bytes, salt.Length, input.Length);
            }

            using (var algorithm = SHA256.Create())
            {
                // remove dashes returned from the BitConverter to get the hex representation
                return BitConverter.ToString(algorithm.ComputeHash(bytes)).Replace("-", string.Empty);
            }
        }
    }
}
