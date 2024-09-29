
namespace Utils.Tools;

public static class RandomTools
{
    public static string GeneratePassword(this int length)
    {
        const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        Random random = new Random();
        return new string(Enumerable.Repeat(validChars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static Guid GenerateGuid()
    {
        return Guid.NewGuid();
    }
}
