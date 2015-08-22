using System.Net.Http;

namespace GroundJobs.Services.Geocoding
{
    public static class HttpHelper
    {
        public static string GetHtmlString(string url)
        {
            var client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            var html = client.GetStringAsync(url);
            html.Wait();
            return html.Result;
        }

        public static dynamic GetJson(string url)
        {
            var data = GetHtmlString(url);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(data);
        }
    }
}