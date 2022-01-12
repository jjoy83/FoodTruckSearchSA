using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckBingMapClient
{
    public interface IFoodTruckBingMapClient
    {
        IEnumerable<BingMapSearchResponse> GetNearestLocationFromBing(string latitude, string longitude);
    }
}
