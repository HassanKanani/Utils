using Newtonsoft.Json;
namespace Utils.Tools;
public static class SerializerTools
{
    public static string SerializeToJson<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T DeserializeFromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
