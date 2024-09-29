
using System.Text;

public static class HttpHelper
{
    private static readonly HttpClient _httpClient = new HttpClient();

    // متد جنریک برای ارسال درخواست GET
    public static async Task<T> GetAsync<T>(string url)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // بررسی وضعیت موفقیت‌آمیز بودن درخواست
            string responseBody = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody); // سریال‌سازی پاسخ به نوع جنریک
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return default;
        }
    }

    // متد جنریک برای ارسال درخواست POST
    public static async Task<T> PostAsync<T>(string url, object data)
    {
        try
        {
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode(); // بررسی وضعیت موفقیت‌آمیز بودن درخواست
            string responseBody = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseBody); // سریال‌سازی پاسخ به نوع جنریک
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return default;
        }
    }
}