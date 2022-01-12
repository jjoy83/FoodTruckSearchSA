using HttpClientWrapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckConsoleApp
{
    public static class FoodTruckSearchCLI
    {
        private static IHttpClientWrapperClient _client;

        static FoodTruckSearchCLI()
        {
            _client = new HttpClientWrapperClient();

        }

        public static async Task<JObject> SearchFoodTruck(string searchKey, string latitude, string longitude)
        {
            string url = ConfigurationManager.AppSettings["BackendUrl"].ToString();
            var response = await _client.GetJObjectAsync(url);
            return response;
        }
    }
}
