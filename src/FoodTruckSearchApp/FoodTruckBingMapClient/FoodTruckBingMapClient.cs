using System;
using System.Collections.Generic;
using BingMapsRESTToolkit;


namespace FoodTruckBingMapClient
{
    /// <summary>
    ///
    /// </summary>
    public class FoodTruckBingMapClient : IFoodTruckBingMapClient
    {
        private BingMapHttpClient client;
        private string BING_MAP_KEY;

        public FoodTruckBingMapClient(string bingMapKey)
        {
            client = new BingMapHttpClient();
            BING_MAP_KEY = bingMapKey;
        }

        public IEnumerable<BingMapSearchResponse> GetNearestLocationFromBing(string latitude, string longitude)
        {
            client = new BingMapHttpClient();
            string url = @$"https://dev.virtualearth.net/REST/v1/LocalSearch/?query=food&userLocation={latitude},{longitude}&key={BING_MAP_KEY}";

            var response = client.GetJObjectAsync(url) as IEnumerable<BingMapSearchResponse>;
            return response;

        }


    }
}
