using System.Text;
using System.Security.Cryptography;
namespace SNET.Framework.Domain.Extensions;

public static class PasswordHasher
{
    public static string EncryptPassword(this string randomString)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
    public static bool VerifyPassword(this string text, string textComparation)
    {
        return text.EncryptPassword() == textComparation;
    }
}
