using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckSearchFrontEnd.Models
{
    public class FoodTruckSearchFrontendSetting
    {
        public string AllowedHosts { get; set; }

        public string BackendAPIUrl { get; set; }

        public JObject  Logging { get; set; }
    }
}
