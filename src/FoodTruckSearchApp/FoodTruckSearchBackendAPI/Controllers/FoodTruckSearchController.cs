using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckBingMapClient;
using FoodTruckSearchBackendAPI.Settings;
using FoodTruckSearchSodaClient;
using HttpClientWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FoodTruckBackendDataModel;

namespace FoodTruckSearchBackendAPI.Controllers
{
    /// <summary>
    /// This is the food truck search backend api.
    /// This will make calls to bing map search api to get the closest co-ordinates from any point.
    /// This will also make calls to soda api to get the closest 5 food trucks.
    /// Currently Authentication is not hooked up. This can be hooked up using Microsoft identity server.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FoodTruckSearchController : ControllerBase
    {
        private readonly ILogger<FoodTruckSearchController> _logger;
        private readonly IFoodTruckBingMapClient _bingMapClient;
        private readonly IFoodTruckSearchSodaClient _sodaSearchClient;
        private readonly IHttpClientWrapperClient _httpClientWrapperClient;
        private readonly IConfiguration _config;
        private readonly string BING_MAP_KEY;
        private readonly string SODA_APP_TOKEN;

        public FoodTruckSearchController(ILogger<FoodTruckSearchController> logger, IConfiguration config, IFoodTruckBingMapClient foodtruckBingMapClient, IFoodTruckSearchSodaClient foodTruckSearchSodaClient, IHttpClientWrapperClient httpWrapperClient)
        {
            _logger = logger;
            _config = config;
            _bingMapClient = foodtruckBingMapClient;
            _sodaSearchClient = foodTruckSearchSodaClient;
            _httpClientWrapperClient = httpWrapperClient;

            //This can be stored in keyvault and fetched from there.
            SODA_APP_TOKEN = _config.GetSection("SodaAccount").Get<SodaSettting>().AppToken;
            BING_MAP_KEY = _config.GetSection("BingMap").Get<BingMapSetting>().Key;

        }

        [Route("/api/SearchFoodTruck")]
        [HttpPost]
        public async Task<IEnumerable<FoodTruckResponse>> SearchFoodTruck(FoodTruckRequest request)
        {

            List<FoodTruckResponse> foodtruckResponseList = new List<FoodTruckResponse>();
            try
            {
                _logger.LogError($"Making calls to bing map search api with latitude {request.Latitude}, longitude {request.Longitude}");
                var response = await _bingMapClient.GetNearestLocationFromBing(request.Latitude, request.Longitude, BING_MAP_KEY);
                _logger.LogError($"Successfully completed the calls to bing map search api with latitude {request.Latitude}, longitude {request.Longitude}");
                _logger.LogError($"Making calls to soda client api with search text {request.SearchText}");
                var sodaResponse = await _sodaSearchClient.SearchFoodtruckSodaDataByText(request.SearchText, SODA_APP_TOKEN);
                _logger.LogError($"Successfully completed the calls to soda client api search text {request.SearchText}");

                foreach (var item in sodaResponse)
                {
                    //TODO:Add logic to check if the latitude and longitude returned by soda response based on search text is nearby or not by comparing the latitude and longitude returned by bing map api.
                    foodtruckResponseList.Add(new FoodTruckResponse
                    {
                        FoodItems = item.fooditems,
                        Latitude = item.latitude,
                        Longitude = item.longitude
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while calling food truck search api. Error - {ex.Message}, StackTrace - {ex.StackTrace}");
               
            }
            return foodtruckResponseList;
        }
    }
}
