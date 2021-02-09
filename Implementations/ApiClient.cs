using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CarbonIntensity
{
    public static class ApiClient
    {
        public static string ToSimpleDate(this DateTime dt) => dt.ToString("yyyy-MM-dd");

        public static string ToISO8601(this DateTime dt) => dt.ToString("yyyy-MM-ddTHH:mmZ");

        static async Task<string> AsyncQuery(string uri)
        {
            var client = new WebClient();
            return await client.DownloadStringTaskAsync(uri);
        } 

        public static async Task<T> GetAsObjects<T>(string uri, string rootElement = "data")
        {
            JObject response = JObject.Parse(await AsyncQuery(uri));
            return JsonConvert.DeserializeObject<T>(response[rootElement].ToString());
        }
    }
}