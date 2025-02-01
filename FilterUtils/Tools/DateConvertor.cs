using System.Globalization;
using System.Text;
namespace Utils.Tools;
public static class DateConvertor
{

    public static String ToShamsi(this DateTime dateTime)
    {
        PersianCalendar persianCalendar = new();
        return $"{persianCalendar.GetYear(dateTime).ToString("0000")}/{persianCalendar.GetMonth(dateTime).ToString()}/{persianCalendar.GetDayOfMonth(dateTime).ToString("00")}";
    }
    public static String ToShamsiLTR(this DateTime dateTime)
    {
        PersianCalendar persianCalendar = new();
        return $"{persianCalendar.GetDayOfMonth(dateTime).ToString("00")}/{persianCalendar.GetMonth(dateTime).ToString()}/{persianCalendar.GetYear(dateTime).ToString("0000")}";
    }
    public static DateTime ToMiladi(int year, int mounth, int day)
    {
        PersianCalendar persianCalendar = new();
        return persianCalendar.ToDateTime(year, mounth, day, 0, 0, 0, 0);
    }

    public static DateTime? ToMiladi(this string date)
    {
        PersianCalendar persianCalendar = new();
        var details = date.Split('/');

        if (details.Length == 0 || details.Length < 2)
        {
            details = date.Split('-');
        }
        if (details.Length > 3)
        {
            return null;
        }
        int year = int.Parse(details[0]);
        int mount = int.Parse(details[1]);
        int day = int.Parse(details[2]);
        return persianCalendar.ToDateTime(year, mount, day, 0, 0, 0, 0);
    }

    public static int ConvertStringToInt(this string str)
    {
        return int.Parse(str);
    }
    public static Double ConvertStringToDouble(this string str)
    {
        return Double.Parse(str);
    }
    public static string ConvertArabicToPersian(this string input) { StringBuilder result = new StringBuilder(input.Length); foreach (char c in input) { switch (c) { case 'ي': result.Append('ی'); break; case 'ك': result.Append('ک'); break; case 'ى': result.Append('ی'); break; case 'ؤ': result.Append('و'); break; case 'إ': result.Append('ا'); break; case 'أ': result.Append('ا'); break; case 'ٱ': result.Append('ا'); break; case 'ۀ': result.Append('ه'); break; case 'ہ': result.Append('ه'); break; default: result.Append(c); break; } } return result.ToString(); }








}

