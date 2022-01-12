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
    public class FoodTruckSearchSodaClient : IFoodTruckSearchSodaClient
    {
        private SodaClient _client;
        private const string SODA_URI = "data.smgov.net";
        private const string SODA_DATASET = "rqzj-sfat";
        public FoodTruckSearchSodaClient()
        {
            
        }

        public async Task<IEnumerable<FoodTruckDataModel>> SearchFoodtruckSodaDataByText(string searchKey, string appToken)
        {
            _client = new SodaClient(SODA_URI, appToken);

            return await Task.Run(() =>
            {
                var dataset = _client.GetResource<Dictionary<string, object>>(SODA_DATASET);

                //collections of an arbitrary type can be returned
                //using SoQL and a fluent query building syntax
                var soql = new SoqlQuery().Select("Applicant", "Address", "FoodItems", "Latitude", "Longitude")
                                          .Where("status = APPROVED")
                                          .FullTextSearch(searchKey)
                                          .Limit(5);

                var results = dataset.Query<FoodTruckDataModel>(soql);
                
                return results;
            });

        }


    }



}
