using System.Security.Cryptography;
using System.Text;

namespace WebApi.Business.src.Shared
{
    public class PasswordService
    {
        public static void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt)
        {
            var hmac = new HMACSHA256(); // used to encode the password. It would generate random key, and use that key to encode. 
            salt = hmac.Key;
            hashedPassword = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
        }

        public static bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt)
        {
            var hmac = new HMACSHA256(salt); // created with an assigned key
            var hashedOriginal = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
            return hashedOriginal == hashedPassword;
        }
    }
}