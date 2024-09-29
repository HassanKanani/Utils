

namespace Utils.Tools;

public class LogTools
{
    public static void Log( string message, string level = "INFO")
    {
        string logMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{level}] {message}";
        File.AppendAllText("logfile.txt", logMessage + Environment.NewLine);
    }

    public static void LogError(string message)
    {
        Log(message, "ERROR");
    }
}
