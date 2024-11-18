using System.Text;
using Utils.Tools;
namespace HttpHelperTools;
public static class HttpHelper
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.DeserializeFromJson<T>();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return default;
        }
    }

    public static async Task<T?> PostAsync<T>(string url, object data)
    {
        try
        {
            string jsonData = data.SerializeToJson();
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode(); 
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody.DeserializeFromJson<T>(); 
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return default;
        }
    }
}