using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckSearchSodaClient
{
    public interface IFoodTruckSearchSodaClient
    {
        IEnumerable<FoodTruckDataModel> SearchFoodtruckSodaDataByText(string searchKey);

        IEnumerable<FoodTruckDataModel> SearchFoodtruckSodaDataByLocation(string latitude, string longitude);
    }
}
