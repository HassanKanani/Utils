namespace Utils.Tools;
public  class ReportTools
{
    public static void ExportToCsv<T>(List<T> data, string filePath)
    {
        var properties = typeof(T).GetProperties();
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine(string.Join(",", properties.Select(p => p.Name)));

            foreach (var item in data)
            {
                sw.WriteLine(string.Join(",", properties.Select(p => p.GetValue(item, null))));
            }
        }
    }
}
