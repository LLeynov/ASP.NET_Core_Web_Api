using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountHelper
{
    public static class PasswordUtils
    {
        private const string SecretKey = "B=o1№#lO@D1m19SkuF10On!";

        public static (string passwordSalt, string passwordHash) CreatePasswordHash(string password)
        {
            byte[] buffer = new byte[16];
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(buffer);

            string passwordSalt = Convert.ToBase64String(buffer);
            string passwordHash = GetPasswordHash(password, passwordSalt);
            return (passwordSalt, passwordHash);
        }

        public static string GetPasswordHash(string password, string passwordSalt)
        {
            password = $"{password}.|.{passwordSalt}]122{SecretKey}";
            byte[] buffer = Encoding.UTF8.GetBytes(password);

            SHA512 sha512 = new SHA512Managed();
            byte[] passwordHash = sha512.ComputeHash(buffer);
            return Convert.ToBase64String(passwordHash);
        }

        public static bool VerifyPassword(string password, string passwordSalt, string passwordHash)
        {
            return GetPasswordHash(password, passwordSalt) == passwordHash;
        }
    }
}
