using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckBackendDataModel
{
    /// <summary>
    /// Data model for Food truck Request
    /// </summary>
    public class FoodTruckRequest
    {
        public string SearchText { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

    }
}
