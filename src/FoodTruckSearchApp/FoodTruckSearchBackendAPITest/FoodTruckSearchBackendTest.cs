using FoodTruckSearchBackendAPI.Controllers;
using FoodTruckSearchBackendAPI.Settings;
using FoodTruckSearchSodaClient;
using HttpClientWrapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using Xunit;
using System.Collections.Generic;
using System.IO;

namespace FoodTruckBackendAPITest
{
    public class FoodTruckSearchBackendTest
    {
        private readonly IConfiguration _config;

        public FoodTruckSearchBackendTest()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
        }
        [Fact]
        public async void SearchFoodTruck_WithResultsTest()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<FoodTruckSearchController>>();
          
            var mockHttpWrapper = new Mock<IHttpClientWrapperClient>();
            mockHttpWrapper.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            var mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(It.IsAny<IEnumerable<FoodTruckDataModel>>());

            string searchText = "test";
            string latitude = "test";
            string longitude = "test";

            //Act
            var foodTruckSearchController = new FoodTruckSearchController(mockLogger.Object, _config, mockSodaClient.Object, mockHttpWrapper.Object);
            var response = await foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);

            //Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
        }

        [Fact]
        public async void SearchFoodTruck_WithMissingParameterSearchText()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<FoodTruckSearchController>>();

            var mockHttpWrapper = new Mock<IHttpClientWrapperClient>();
            mockHttpWrapper.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            var mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(It.IsAny<IEnumerable<FoodTruckDataModel>>());

            var mockConfig = new Mock<IConfiguration>();
    
            string searchText = "";
            string latitude = "test";
            string longitude = "test";

            //Act
            var foodTruckSearchController = new FoodTruckSearchController(mockLogger.Object, _config, mockSodaClient.Object, mockHttpWrapper.Object);
            var response = await foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);

            //Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal("Please provide parameter search text.\n", response.ErrorMessage);
        }

        [Fact]
        public async void SearchFoodTruck_WithMissingParameterlatitude()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<FoodTruckSearchController>>();
            var mockHttpWrapper = new Mock<IHttpClientWrapperClient>();
            mockHttpWrapper.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            var mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(It.IsAny<IEnumerable<FoodTruckDataModel>>());

            var mockConfig = new Mock<IConfiguration>();

            string searchText = "test";
            string latitude = "";
            string longitude = "test";

            //Act
            var foodTruckSearchController = new FoodTruckSearchController(mockLogger.Object, _config, mockSodaClient.Object, mockHttpWrapper.Object);
            var response = await foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);

            //Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal("Please provide parameter latitude.\n", response.ErrorMessage);
        }

        [Fact]
        public async void SearchFoodTruck_WithMissingParameterlongitude()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<FoodTruckSearchController>>();

            var mockHttpWrapper = new Mock<IHttpClientWrapperClient>();
            mockHttpWrapper.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            var mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(It.IsAny<IEnumerable<FoodTruckDataModel>>());

            var mockConfig = new Mock<IConfiguration>();
         
            string searchText = "test";
            string latitude = "test";
            string longitude = "";

            //Act
            var foodTruckSearchController = new FoodTruckSearchController(mockLogger.Object, _config, mockSodaClient.Object, mockHttpWrapper.Object);
            var response = await foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);

            //Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal("Please provide parameter longitude.\n", response.ErrorMessage);
        }

        [Fact]
        public async void SearchFoodTruck_WithNoResults()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<FoodTruckSearchController>>();

            var mockHttpWrapper = new Mock<IHttpClientWrapperClient>();
            mockHttpWrapper.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            var result = new List<FoodTruckDataModel>();
            var mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result);

            var mockConfig = new Mock<IConfiguration>();
         
            string searchText = "test";
            string latitude = "test";
            string longitude = "test";
          

            //Act
            var foodTruckSearchController = new FoodTruckSearchController(mockLogger.Object, _config, mockSodaClient.Object, mockHttpWrapper.Object);
            var response = await foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);

            //Assert
            Assert.True(response.Success);
            Assert.NotNull(response);
            Assert.Equal("No results found for this search criteria.", response.ErrorMessage);
        }

        [Fact]
        public async void SearchFoodTruck_WithException()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<FoodTruckSearchController>>();
          
            var mockHttpWrapper = new Mock<IHttpClientWrapperClient>();
            mockHttpWrapper.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            var result = new List<FoodTruckDataModel>();
            var mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            var mockConfig = new Mock<IConfiguration>();

            string searchText = "test";
            string latitude = "test";
            string longitude = "test";


            //Act
            var foodTruckSearchController = new FoodTruckSearchController(mockLogger.Object, _config, mockSodaClient.Object, mockHttpWrapper.Object);
            var response = await foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);

            //Assert
            Assert.False(response.Success);
            Assert.NotNull(response);
        }
    }
}
