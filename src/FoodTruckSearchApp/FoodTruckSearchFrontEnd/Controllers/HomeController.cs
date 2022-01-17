using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodTruckSearchFrontEnd.Models;
using HttpClientWrapper;
using Microsoft.Extensions.Options;
using System.Text;
using Newtonsoft.Json;

namespace FoodTruckSearchFrontEnd.Controllers
{
    /// <summary>
    /// This is the home controller for the front end 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _backendAPIUrl;

        public HomeController(ILogger<HomeController> logger, IOptions<FoodTruckSearchOptions> foodTruckSearchOptions)
        {
            _logger = logger;
            _backendAPIUrl = foodTruckSearchOptions.Value.Uri;
        }

        public IActionResult Index()
        {
            IEnumerable<SearchResponseViewModel> model = new List<SearchResponseViewModel>();
            return View(model);
        }

        //public async Task<IActionResult> Search(string searchText, string latitude, string longitude)
        //{
        //    if (!string.IsNullOrEmpty(searchText))
        //    {
        //        SearchResponseDataModel responseViewModel = null;
        //        IHttpClientWrapperClient client = new HttpClientWrapperClient();
        //        string url = _backendAPIUrl;

        //        //Since this code is duplicated in Console app, we could refactor to a utility class.
        //        StringBuilder sb = new StringBuilder(url);
        //        sb.Append("?");
        //        sb.Append($"&searchText={Uri.EscapeUriString(searchText)}");
        //        sb.Append($"&latitude={Uri.EscapeUriString(latitude)}");
        //        sb.Append($"&longitude={Uri.EscapeUriString(longitude)}");
        //        var response = await client.GetJObjectAsync(sb.ToString());

        //        if (response != null)
        //        {
        //            responseViewModel = JsonConvert.DeserializeObject<SearchResponseDataModel>(response.ToString());
        //        }

        //        return View(responseViewModel.SearchResponseViewModelList);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
