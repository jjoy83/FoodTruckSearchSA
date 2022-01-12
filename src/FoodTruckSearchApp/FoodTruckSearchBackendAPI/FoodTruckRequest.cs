using System;

namespace FoodTruckSearchBackendAPI
{
    public class FoodTruckRequest
    {
        public string SearchText { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
       
    }
}
