using Microsoft.VisualStudio.TestTools.UnitTesting;
using FoodTruckBingMapClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using MockHttpClient;
using System.Threading.Tasks;

namespace FoodTruckBingMapClient.Tests
{

    [TestClass()]
    public class FoodTruckBingMapClientTests
    {
        private IFoodTruckBingMapClient client;
        private readonly string BINGMAP_KEY = "AtgqTzjl5buu80RwoChi-55k1NkC18DGvQCwSnjICKIkQBfx-xG3pWFtlrGM9kzk";

        [TestInitialize]
        public void TestInitialize()
        {
            client = new FoodTruckBingMapClient(BINGMAP_KEY);
        }

        [TestMethod()]
        public void GetNearestLocationFromBingTest()
        {

            string longitude = "28.121456";
            string latitude = "-82.462074";
            MockHttpClient.MockHttpClient mockClient = new MockHttpClient.MockHttpClient();

            mockClient
            .When("https://dev.virtualearth.net/REST/v1/LocalSearch/*")
                .Then(req => new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                      .WithJsonContent(new
                      {
                          entityType = "Test",
                          name = "Test",
                          Website = "Test",
                          geoCodePoint = "longitude,latitude",
                          address = "test",
                          phoneNumber = "123"

                      }));


            var response = client.GetNearestLocationFromBing(longitude, latitude);

            Assert.IsNotNull(response);

        }
    }
}