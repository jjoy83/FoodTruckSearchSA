using HttpClientWrapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckConsoleApp
{
    /// <summary>
    /// This is the class used to make calls to backend food truck search api based on the input parameters
    /// </summary>
    public static class FoodTruckSearchCLI
    {
        private static IHttpClientWrapperClient _client;

        static FoodTruckSearchCLI()
        {
            _client = new HttpClientWrapperClient();

        }

        /// <summary>
        /// This method will make a call to the food truck search backend api.
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static async Task<JObject> SearchFoodTruck(string searchKey, string latitude, string longitude)
        {
            JObject response = null;
            try
            {
                string url = ConfigurationManager.AppSettings["BackendUrl"].ToString();
                StringBuilder sb = new StringBuilder(url);
                sb.Append("?");
                sb.Append($"&searchText={Uri.EscapeUriString(searchKey)}");
                sb.Append($"&latitude={Uri.EscapeUriString(latitude)}");
                sb.Append($"&longitude={Uri.EscapeUriString(longitude)}");

                if (Uri.IsWellFormedUriString(sb.ToString(), UriKind.Absolute))
                {
                    response = await _client.GetJObjectAsync(url);

                }
                else
                {
                    throw new Exception("The parameters are not valid. Please try again. Type  -h for help...");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
