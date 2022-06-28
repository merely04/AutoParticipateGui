using System.Net;

namespace AutoParticipateGui.Services
{
    public class ApiService
    {
        private const string UpdateUrl = "https://bot-father.ru/download";

        public static void DownloadUpdate(string fileName)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, "python-requests/2.27.1");
                wc.DownloadFile(UpdateUrl, fileName);
            }
        }
    }
}