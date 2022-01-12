
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This is the response model for bing map location search api
/// </summary>
namespace FoodTruckBingMapClient
{
    public class BingMapSearchResponse
    {
        public string entityType { get; set; }
        public string name { get; set; }
        public string Website { get; set; }
        public string geocodePoints { get; set; }
        public string address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
