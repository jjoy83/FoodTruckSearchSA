using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckBackendDataModel
{
    /// <summary>
    /// Data model for food truck response.
    /// </summary>
    public class FoodTruckResponse
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string FoodItems { get; set; }

        public string X { get; set; }

        public string Y { get; set; }
    }
}
