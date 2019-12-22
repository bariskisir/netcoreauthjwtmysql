using System;
using System.Security.Cryptography;
using System.Text;
namespace NetCoreAuthJwtMySql.Utils
{
    public static class CryptoUtil
    {
        public static string GenerateSalt()
        {
            var salt = Guid.NewGuid().ToString();
            return salt;
        }
        private static string Hash(string text)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(text))
            {
                var encoding = new UTF8Encoding();
                var sha512Provider = new SHA512CryptoServiceProvider();
                var data = sha512Provider.ComputeHash(encoding.GetBytes(text));
                result = ByteToHex(data);
            }
            return result;
        }
        public static string HashMultiple(string password, string salt)
        {
            var hashedString = password + salt;
            for (int i = 0; i < 7; i++)
            {
                hashedString = Hash(hashedString);
            }
            return hashedString;
        }
        public static string ByteToHex(byte[] input)
        {
            var stringBuilder = new StringBuilder(string.Empty);
            for (var i = 0; i < input.Length; i++)
            {
                stringBuilder.Append(input[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }
        public static bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            var result = false;
            var hashed = HashMultiple(password, salt);
            if (hashedPassword == hashed)
            {
                result = true;
            }
            return result;
        }
    }
}
