using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Threading.Tasks;


namespace WeatherPOCTest
{
    [TestClass]
    public class WeatherTest1
    {
        public WeatherTest1()
        {
        }

        [TestMethod]
        public async Task GetWeatherByLocationSuccessResponse()
        {
            string queryURL = $"http://api.openweathermap.org/data/2.5/weather?q=egypt&units=metric&appid=ea09d6f51ff61d0483fd36e3719d1a4b";
            RestClient client = new RestClient(queryURL);
            RestRequest request = new RestRequest();
            RestResponse response = await client.ExecuteAsync(request);

            if (response != null && response.IsSuccessful)
            {
                var content = response.Content;
                Console.Write(content);
                if (!content.Contains("Egypt"))
                    Assert.Fail(content);
            }
            else
                Assert.Fail("not found");
        }
    }
}
