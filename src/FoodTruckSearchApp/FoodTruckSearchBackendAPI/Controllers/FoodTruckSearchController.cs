using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckBingMapClient;
using FoodTruckSearchSodaClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodTruckSearchBackendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodTruckSearchController : ControllerBase
    {
        private readonly ILogger<FoodTruckSearchController> _logger;
        private IFoodTruckBingMapClient _bingMapClient;
        private IFoodTruckSearchSodaClient _sodaSearchClient;

        public FoodTruckSearchController(ILogger<FoodTruckSearchController> logger)
        {
            _logger = logger;
            _bingMapClient = new FoodTruckBingMapClient.FoodTruckBingMapClient("");
            _sodaSearchClient = new FoodTruckSearchSodaClient.FoodTruckSearchSodaClient("");
        }

        [HttpGet]
        public IEnumerable<FoodTruckResponse> Get(FoodTruckRequest request)
        {
            List<FoodTruckResponse> foodtruckResponseList = new List<FoodTruckResponse>();
            var response = _bingMapClient.GetNearestLocationFromBing(request.Latitude, request.Longitude);
            var sodaResponse = _sodaSearchClient.SearchFoodtruckSodaDataByText(request.SearchText);
            
            foreach(var item in sodaResponse)
            {
                //Add logic to check if the latitude and longitude returned by soda response based on search text is nearby or not by comparing the latitude and longitude returned by bing map api.
                foodtruckResponseList.Add(new FoodTruckResponse
                {
                    FoodItems = item.fooditems,
                    Latitude = item.latitude,
                    Longitude = item.longitude
                });
            }
            return foodtruckResponseList;
        }
    }
}
