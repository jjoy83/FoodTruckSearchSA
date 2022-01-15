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
        public IEnumerable<FoodTruckData> FoodTruckDataList { get; set; } = new List<FoodTruckData>();

        public bool Success { get; set; } 

        public string ErrorMessage { get; set; }
    }
}
