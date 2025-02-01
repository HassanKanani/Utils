
using System.Text.RegularExpressions;
using Utils.Models;
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

    public static bool IsEmpty(this Guid Value)
    {
        if (Value == Guid.Empty) return true;
        else return false;
    }
    public static bool IsNumeric(dynamic type)
    {
        if (type is int || type is long || type is float || type is double || type is decimal)
        {
            return true;
        }
        return false;
    }
    public static ApiResponse<bool> ConcurrencyInterference(byte[] RowVersion, byte[] RowVersionFromRequest, string message = "The entity has been modified by someone else")
    {
        if (!RowVersion.SequenceEqual(RowVersionFromRequest))
        {
            return ApiResponse<bool>.CreateErrorResponse(message);
        }
        return ApiResponse<bool>.CreateSuccessResponse();
    }
    public static bool IsValidMobileNumber(this string mobileNumber)
    {
        if (string.IsNullOrEmpty(mobileNumber))
            return false;

        mobileNumber = mobileNumber.Trim();

        string pattern = @"^(?:\+98|0098|98|0)?9\d{9}$";

        if (!Regex.IsMatch(mobileNumber, pattern))
            return false;

        int lengthWithoutPrefix = mobileNumber.Replace("+98", "0")
                                              .Replace("0098", "0")
                                              .Replace("98", "0").Length;
        return lengthWithoutPrefix == 11;
    }


}
