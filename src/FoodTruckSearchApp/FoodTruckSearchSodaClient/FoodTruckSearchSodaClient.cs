using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SODA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckSearchSodaClient
{
    /// <summary>
    /// This is the food truck search soda client to query the food truck soda database.
    /// </summary>
    public class FoodTruckSearchSodaClient : IFoodTruckSearchSodaClient
    {
        private SodaClient _client;
        private const string SODA_URI = "data.sfgov.org";
        private const string SODA_DATASET = "rqzj-sfat";
        public FoodTruckSearchSodaClient()
        {
            
        }

        public async Task<IEnumerable<FoodTruckDataModel>> SearchFoodtruckSodaDataByText(string searchKey, string appToken, string latitude, string longitude)
        {
            _client = new SodaClient(SODA_URI, appToken);

            return await Task.Run(() =>
            {
                var dataset = _client.GetResource<Dictionary<string, object>>(SODA_DATASET);

                //collections of an arbitrary type can be returned
                //using SoQL and a fluent query building syntax
                var soql = new SoqlQuery().Select("applicant", "address", "fooditems", "locationdescription", "latitude ", "longitude", "x", "y")
                                          .Where("status = APPROVED")
                                          .Where($"within_circle(location,{latitude} , {longitude}, 50)")
                                          .FullTextSearch(searchKey)
                                          .Limit(5);

                var results = dataset.Query<FoodTruckDataModel>(soql);
                
                return results;
            });

        }


    }



}
