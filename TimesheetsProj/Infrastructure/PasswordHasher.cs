using System.Text;
using System.Security.Cryptography;

namespace TimesheetsProj.Infrastructure
{
    public class PasswordHasher
    {
        public static byte[] GetPasswordHash(string password)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                return sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
            }
        }

        public static bool IsPasswordCorrect(string password, byte[] dbPassword)
        {
            byte[] passwordHash = GetPasswordHash(password);
            bool result = dbPassword.SequenceEqual(passwordHash);

            return result;
        }
    }
}
