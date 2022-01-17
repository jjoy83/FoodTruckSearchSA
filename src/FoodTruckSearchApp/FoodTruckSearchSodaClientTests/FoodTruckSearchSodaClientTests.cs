using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodTruckSearchSodaClient.Tests
{
    [TestClass()]
    public class FoodTruckSearchSodaClientTests
    {
        [TestMethod()]
        public void SearchFoodtruckSodaDataTest()
        {
           //TODO:Need to figure out whether we can mock soda client.
           //Best approach would be to create a wrapper for soda client and then mock the wrapper.
            Assert.AreEqual(1,1);
        }
    }
}