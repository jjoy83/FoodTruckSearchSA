using System.Collections.Generic;
using System.Threading.Tasks;
using HttpClientWrapper;

/// <summary>
/// This is the Bing Map Search Client 
/// </summary>
namespace FoodTruckBingMapClient
{
    /// <summary>
    ///This class is responsible for making search request to bing map local search
    /// The initial idea was to use bing map local search api to get the boundaries of the co-ordinates and then uses that to filter the result further from soda api data
    /// but it looks likr soda data already supports a filtering using within_circle for location data types.
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
