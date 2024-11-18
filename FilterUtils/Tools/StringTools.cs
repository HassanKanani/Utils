using System.Text.RegularExpressions;
namespace Utils.Tools;
public static class StringTools
{
    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToLowerInvariant(input[0]) + input.Substring(1);
    }
    public static bool IsNullOrEmpty(this string? input)
    {
        return string.IsNullOrEmpty(input);
    }
    public static bool IsEmailValid(this string email)
    {
        Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return regex.IsMatch(email);
    }
    public static string FormatWithCommas(this int number)
    {
        return number.ToString("N0");
    }
    public static bool IsPhoneNumberValid(this string phoneNumber)
    {
        Regex regex = new Regex(@"^\+?\d{10,15}$");
        return regex.IsMatch(phoneNumber);
    }
    public static bool IsEmpty(this Guid Value)
    {
        if(Value==Guid.Empty) return true;
        else return false;
    }
    public static bool IsNumeric(dynamic type)
    {
        if (type is int ||type is long|| type is float|| type is double|| type is decimal)
        {
            return true;
        }
        return false; 
    }



}
