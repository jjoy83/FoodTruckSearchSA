using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckBingMapClient
{
    /// <summary>
    /// This is the interface for bing map search client.
    /// </summary>
    public interface IFoodTruckBingMapClient
    {
        Task<IEnumerable<BingMapSearchResponse>> GetNearestLocationFromBing(string latitude, string longitude, string bingMapKey);
    }
}
