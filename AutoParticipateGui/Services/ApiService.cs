using System.Net;

namespace AutoParticipateGui.Services
{
    public class ApiService
    {
        private const string ApiUrl = "https://bot-father.ru";

        public static void DownloadUpdate(string fileName)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, "python-requests/2.27.1");
                wc.DownloadFile($"{ApiUrl}/download", fileName);
            }
        }

        public static string GetLastVersion()
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, "python-requests/2.27.1");
                return wc.DownloadString($"{ApiUrl}/last_version");
            }
        }
    }
}