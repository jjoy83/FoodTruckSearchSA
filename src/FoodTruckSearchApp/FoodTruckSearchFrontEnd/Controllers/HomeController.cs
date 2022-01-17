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
            ViewData["BackendURL"] = _backendAPIUrl;
          
            return View();
        }

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
