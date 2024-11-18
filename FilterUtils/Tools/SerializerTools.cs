using Newtonsoft.Json;
namespace Utils.Tools;
public static class SerializerTools
{
    public static string SerializeToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T? DeserializeFromJson<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
