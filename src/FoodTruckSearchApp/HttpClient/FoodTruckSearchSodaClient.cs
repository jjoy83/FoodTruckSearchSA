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
        private SodaClient client;
        //private const string APP_TOKEN = "RpdIpNijZuSD9e8d1eHG69DvN";
        private const string SODA_URI = "data.smgov.net";
        private const string SODA_DATASET = "rqzj-sfat";
        public FoodTruckSearchSodaClient(string appToken)
        {
            client = new SodaClient(SODA_URI, appToken);
        }

        public IEnumerable<FoodTruckDataModel> SearchFoodtruckSodaDataByText(string searchKey)
        {
            var dataset = client.GetResource<Dictionary<string, object>>(SODA_DATASET);

            // Resource objects read their own data
            var first5Rows = dataset.GetRows(5);

            //collections of an arbitrary type can be returned
            //using SoQL and a fluent query building syntax
            var soql = new SoqlQuery().Select("Applicant", "Address", "FoodItems", "Latitude", "Longitude")
                                      .Where("status = APPROVED")
                                      .FullTextSearch(searchKey);

            var results = dataset.Query<FoodTruckDataModel>(soql);
            return results;

        }

        public IEnumerable<FoodTruckDataModel> SearchFoodtruckSodaDataByLocation(string latitude, string longitude)
        {
            var dataset = client.GetResource<Dictionary<string, object>>(SODA_DATASET);

            // Resource objects read their own data
            var first5Rows = dataset.GetRows(5);

            //collections of an arbitrary type can be returned
            //using SoQL and a fluent query building syntax
            var soql = new SoqlQuery().Select("Applicant", "Address", "FoodItems", "Latitude", "Longitude")
                                      .Where($"status = APPROVED and Latitude = {latitude} and Longitude = {longitude}");

            var results = dataset.Query<FoodTruckDataModel>(soql);
            return results;

        }

    }



}
