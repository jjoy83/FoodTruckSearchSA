using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpClientWrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using MockHttpClient;
using System.Net.Http;

namespace HttpClientWrapper.Tests
{
    [TestClass()]
    public class HttpClientWrapperTests
    {
        [TestMethod()]
        public void GetJObjectAsyncTest()
        {
            var mockHttpClient = new MockHttpClient.MockHttpClient();
            IHttpClientWrapperClient httpWrapperClient = new HttpClientWrapperClient();

            mockHttpClient
            .When(req => req.HasHeader("accept", "application/json"))
            .Then(req => new HttpResponseMessage()
            .WithHeader("version", "1")
            .WithJsonContent(new
            {
                FirstName = "John",
                LastName = "Doe"
            }));

            var response = httpWrapperClient.GetJObjectAsync("uri");

            Assert.IsNotNull(response);
        }
    }
}