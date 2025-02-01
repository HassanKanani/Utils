using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace Utils.Tools;

public static class EnumUtils
{
    public static string GetEnumDisplayName(this Enum value)
    {
        try
        {
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            var displayAttribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                                             .FirstOrDefault() as DisplayAttribute;
            if (displayAttribute != null)
            {
                if (!string.IsNullOrEmpty(displayAttribute.Name))
                    return displayAttribute.Name;
            }
            return string.Empty;
        }
        catch (Exception)
        {

            return string.Empty;
        }

    }
    public static int GetNumericValue(this Enum value)
    {
        return Convert.ToInt32(value);
    }
    public static Enum GetEnumByNumeric(this int? value)
    {
        if (value == 0)
        {
            return null;
        }
        return (Enum)Enum.ToObject(typeof(Enum), value);
    }
    public static Dictionary<int, string> GetEnumDictionary<EnumT>() where EnumT : Enum
    {
        var dictionary = new Dictionary<int, string>();
        foreach (EnumT type in Enum.GetValues(typeof(EnumT)))
        {
            dictionary[type.GetNumericValue()] = type.GetEnumDisplayName();
        }
        return dictionary;
    }
    public static Dictionary<string, int> GetEnumDictionaryStringInt<EnumT>() where EnumT : Enum
    {
        var dictionary = new Dictionary<string, int>();
        foreach (EnumT type in Enum.GetValues(typeof(EnumT)))
        {
            dictionary[type.GetEnumDisplayName()] = type.GetNumericValue();
        }
        return dictionary;
    }
    public static TEnum? GetEnumValueOrNull<TEnum>(int value) where TEnum : struct, Enum
    {
        // بررسی اینکه مقدار داده شده در مقادیر enum موجود است یا خیر
        if (Enum.IsDefined(typeof(TEnum), value))
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        // اگر موجود نبود، null برگرداند
        return null;
    }


}
