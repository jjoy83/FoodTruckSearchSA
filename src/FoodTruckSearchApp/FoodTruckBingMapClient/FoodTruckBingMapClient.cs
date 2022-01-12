using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BingMapsRESTToolkit;
using HttpClientWrapper;

/// <summary>
/// This is the Bing Map Search Client 
/// </summary>
namespace FoodTruckBingMapClient
{
    /// <summary>
    ///This class is responsible for making search request to bing map local search
    ///Tried to use the BingMapApiRestKit from Nuget. But somehow the LocationCog search request is missing. Hence used the httpClient approach.
    /// </summary>
    public class FoodTruckBingMapClient : IFoodTruckBingMapClient
    {
        private IHttpClientWrapperClient _client;

        public FoodTruckBingMapClient(IHttpClientWrapperClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<BingMapSearchResponse>> GetNearestLocationFromBing(string latitude, string longitude, string bingMapKey)
        {
            string url = @$"https://dev.virtualearth.net/REST/v1/LocalSearch/?query=food&userLocation={latitude},{longitude}&key={bingMapKey}";
            var response = await _client.GetJObjectAsync(url) as IEnumerable<BingMapSearchResponse>;
            return response;

        }

    }
}
