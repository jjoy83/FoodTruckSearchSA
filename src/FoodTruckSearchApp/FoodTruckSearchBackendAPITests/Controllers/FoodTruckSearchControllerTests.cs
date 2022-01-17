using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Logging;
using HttpClientWrapper;
using Newtonsoft.Json.Linq;
using FoodTruckSearchSodaClient;
using FoodTruckBackendDataModel;
using Microsoft.Extensions.Configuration;
using FoodTruckSearchBackendAPI.Settings;

namespace FoodTruckSearchBackendAPI.Controllers.Tests
{
    [TestClass()]
    public class FoodTruckSearchControllerTests
    {
        private FoodTruckSearchController _foodTruckSearchController;
        private Mock<ILogger<FoodTruckSearchController>> _mockLogger;
        private Mock<IHttpClientWrapperClient> _mockHttpWrapper;
        private Mock<IFoodTruckSearchSodaClient> _mockSodaClient;
        private Mock<IConfiguration> _mockConfig;

        [TestInitialize()]
        public void FoodTruckSearchControllerInitialize()
        {
            _mockLogger = new Mock<ILogger<FoodTruckSearchController>>();
            _mockLogger.Setup(x => x.LogInformation(It.IsAny<string>()));
            _mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            _mockHttpWrapper = new Mock<IHttpClientWrapperClient>();
            _mockHttpWrapper.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            _mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            _mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(It.IsAny<IEnumerable<FoodTruckDataModel>>());

            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(x => x.GetSection(It.IsAny<string>()).Get<SodaSettting>()).Returns(It.IsAny<SodaSettting>());
        }

        [TestMethod()]
        public async void SearchFoodTruckWithResultsTest()
        {
            string searchText = "test";
            string latitude = "test";
            string longitude = "test";

            _foodTruckSearchController = new FoodTruckSearchController(_mockLogger.Object, _mockConfig.Object, _mockSodaClient.Object, _mockHttpWrapper.Object);
            var response = await _foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);
            Assert.AreEqual<bool>(response.Success, true);
            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public async void SearchFoodTruckWithMissingParameterSearchText()
        {
            string searchText = "";
            string latitude = "test";
            string longitude = "test";

            _foodTruckSearchController = new FoodTruckSearchController(_mockLogger.Object, _mockConfig.Object, _mockSodaClient.Object, _mockHttpWrapper.Object);
            FoodTruckResponse response = await _foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);
            Assert.AreEqual<bool>(response.Success, true);
            Assert.AreEqual(response.ErrorMessage, "Please provide parameter search text.\n");
        }

        [TestMethod()]
        public async void SearchFoodTruckWithMissingParameterLatitude()
        {
            string searchText = "test";
            string latitude = "";
            string longitude = "test";

            _foodTruckSearchController = new FoodTruckSearchController(_mockLogger.Object, _mockConfig.Object, _mockSodaClient.Object, _mockHttpWrapper.Object);
            FoodTruckResponse response = await _foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);
            Assert.AreEqual<bool>(response.Success, true);
            Assert.AreEqual(response.ErrorMessage, "Please provide parameter latitude.\n");
        }

        [TestMethod()]
        public async void SearchFoodTruckWithMissingNoResults()
        {
            string searchText = "test";
            string latitude = "test";
            string longitude = "test";

            var result = new List<FoodTruckDataModel>();

            _mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            _mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result);

            _foodTruckSearchController = new FoodTruckSearchController(_mockLogger.Object, _mockConfig.Object, _mockSodaClient.Object, _mockHttpWrapper.Object);
            FoodTruckResponse response = await _foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);
            Assert.AreEqual<bool>(response.Success, true);
            Assert.AreEqual(response.ErrorMessage, "No results found for this search criteria.");
        }

        [TestMethod()]
        public async void SearchFoodTruckWithException()
        {
            string searchText = "test";
            string latitude = "test";
            string longitude = "test";

            var result = new List<FoodTruckDataModel>();

            _mockSodaClient = new Mock<IFoodTruckSearchSodaClient>();
            _mockSodaClient.Setup(x => x.SearchFoodtruckSodaDataByText(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            _foodTruckSearchController = new FoodTruckSearchController(_mockLogger.Object, _mockConfig.Object, _mockSodaClient.Object, _mockHttpWrapper.Object);
            FoodTruckResponse response = await _foodTruckSearchController.SearchFoodTruck(searchText, latitude, longitude);
            Assert.AreEqual<bool>(response.Success, false);

        }
    }
}