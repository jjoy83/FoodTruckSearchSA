using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckSearchFrontEnd.Models
{
    public class SearchResponseDataModel
    {
        public IEnumerable<SearchResponseViewModel> SearchResponseViewModelList { get; set; }

        public bool Success { get; set; }

        public string ErrorMessge { get; set; }
    }
}
