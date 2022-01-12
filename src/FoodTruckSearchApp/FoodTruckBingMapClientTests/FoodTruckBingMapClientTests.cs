using Microsoft.VisualStudio.TestTools.UnitTesting;
using FoodTruckBingMapClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using HttpClientWrapper;
using Newtonsoft.Json.Linq;

namespace FoodTruckBingMapClient.Tests
{

    /// <summary>
    /// These are the unit tests for bing map search client. Right now the basic MSUnit tests are being used for save time
    /// We can probably add Nunit or some other unit testing extension and leverage that.
    /// Used Moq for mocking.
    /// </summary>
    [TestClass()]
    public class FoodTruckBingMapClientTests
    {
        private IFoodTruckBingMapClient client;
        private string BINGMAP_KEY = "dummy-key";

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod()]
        public void GetNearestLocationFromBingTest()
        {

            string longitude = "28.121456";
            string latitude = "-82.462074";

            var mock = new Mock<IHttpClientWrapperClient>();
            mock.Setup(x => x.GetJObjectAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<JObject>());

            client = new FoodTruckBingMapClient(mock.Object);

            var response = client.GetNearestLocationFromBing(longitude, latitude, BINGMAP_KEY);

            mock.Verify(x => x.GetJObjectAsync(It.IsAny<string>()), Times.AtLeastOnce());

        }
    }
}