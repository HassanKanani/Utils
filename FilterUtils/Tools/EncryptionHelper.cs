
using System.Security.Cryptography;
using System.Text;


namespace Utils.Tools;

public static class EncryptionHelper
{
    public static string HashSHA256(this string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
