﻿using System;
using System.Linq;
using System.Threading.Tasks;
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
        //private readonly IFoodTruckBingMapClient _bingMapClient;
        private readonly IFoodTruckSearchSodaClient _sodaSearchClient;
        private readonly IHttpClientWrapperClient _httpClientWrapperClient;
        private readonly IConfiguration _config;
        //private readonly string BING_MAP_KEY;
        private readonly string SODA_APP_TOKEN;

        public FoodTruckSearchController(ILogger<FoodTruckSearchController> logger, IConfiguration config, IFoodTruckSearchSodaClient foodTruckSearchSodaClient, IHttpClientWrapperClient httpWrapperClient)
        {
            _logger = logger;
            _config = config;
            //_bingMapClient = foodtruckBingMapClient;
            _sodaSearchClient = foodTruckSearchSodaClient;
            _httpClientWrapperClient = httpWrapperClient;

            //This can be stored in keyvault and fetched from there.
            SODA_APP_TOKEN = _config.GetSection("SodaAccount").Get<SodaSettting>().AppToken;
            //BING_MAP_KEY = _config.GetSection("BingMap").Get<BingMapSetting>().Key;

        }

        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }

        [Route("SearchFoodTruck")]
        [HttpGet]
        public async Task<FoodTruckResponse> SearchFoodTruck(string searchText, string latitude, string longitude)
        {

            FoodTruckResponse foodtruckResponse = new FoodTruckResponse();

            try
            {
                bool isValid = true;

                if (string.IsNullOrEmpty(searchText))
                {
                    foodtruckResponse.Success = true;
                    foodtruckResponse.ErrorMessage = "Please provide parameter search text.\n";
                    isValid = false;
                }

                if (string.IsNullOrEmpty(latitude))
                {
                    foodtruckResponse.Success = true;
                    foodtruckResponse.ErrorMessage += "Please provide parameter latitude.\n";
                    isValid = false;
                }

                if (string.IsNullOrEmpty(longitude))
                {
                    foodtruckResponse.Success = true;
                    foodtruckResponse.ErrorMessage += "Please provide parameter longitude.\n";
                    isValid = false;
                }

                if (isValid)
                {
                    _logger.LogInformation($"Making calls to soda client api with search text {searchText}");
                    var sodaResponse = await _sodaSearchClient.SearchFoodtruckSodaDataByText(searchText, SODA_APP_TOKEN, latitude, longitude);
                    _logger.LogInformation($"Successfully completed the calls to soda client api search text {searchText}");

                    if (sodaResponse != null && sodaResponse.Count() > 0)
                    {
                        foodtruckResponse.FoodTruckDataList = sodaResponse.Select(item => new FoodTruckData()
                        {
                            FoodItems = item.fooditems,
                            Latitude = item.latitude,
                            Longitude = item.longitude,
                            X = item.x,
                            Y = item.y,
                            Applicant = item.applicant
                        }).ToList();
                        foodtruckResponse.Success = true;
                    }
                    else
                    {
                        foodtruckResponse.Success = true;
                        foodtruckResponse.ErrorMessage = "No results found for this search criteria.";
                    }
                }

            }
            catch (Exception ex)
            {
                foodtruckResponse.Success = false;
                foodtruckResponse.ErrorMessage = ex.Message;
                _logger.LogError($"Error occured while calling food truck search api. Error - {ex.Message}, StackTrace - {ex.StackTrace}");

            }
            return foodtruckResponse;
        }
    }
}
