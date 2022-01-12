using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckSearchSodaClient
{
    public interface IFoodTruckSearchSodaClient
    {
        Task<IEnumerable<FoodTruckDataModel>> SearchFoodtruckSodaDataByText(string searchKey, string appToken);

    }
}
