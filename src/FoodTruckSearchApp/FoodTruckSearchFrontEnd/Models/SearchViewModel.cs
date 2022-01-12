using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruckSearchFrontEnd.Models
{
    public class SearchViewModel : PageModel
    {
        [Required]
        [ModelBinder(Name = "searchText")]
        public string SearchText { get; set; }

        [Required]
        [ModelBinder(Name = "latitude")]
        public string Latitude { get; set; }

        [Required]
        [ModelBinder(Name = "longitude")]
        public string Longitude { get; set; }
    }
}
