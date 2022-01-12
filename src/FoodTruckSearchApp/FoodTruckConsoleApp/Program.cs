using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace FoodTruckConsoleApp
{
    /// <summary>
    /// This is the console app for search food truck.
    /// This is supposed to call the webapi and get the results
    /// We can use the httpClientWrapperClient to call the backend API
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string searchKey = string.Empty;
            string latitude = string.Empty;
            string longitude = string.Empty;

            if (args != null)
            {
                if (!string.IsNullOrEmpty(args[0]) && string.Compare(args[0], "searchFoodTruck") == 0)
                {
                    if (args[1] == "-h")
                    {
                        SearchHelpText();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(args[1]))
                        {
                            searchKey = args[1];
                        }

                        if (!string.IsNullOrEmpty(args[2]))
                        {
                            longitude = args[2];
                        }

                        if (!string.IsNullOrEmpty(args[3]))
                        {
                            latitude = args[3];
                        }

                        JObject response = Task.Run(()=> FoodTruckSearchCLI.SearchFoodTruck(searchKey, latitude, longitude)).Result;
                        Console.WriteLine(response.ToString());
                     
                    }

                }
                else
                {
                    SearchHelpText();
                }
            }


        }
        private static void SearchHelpText()
        {
            Console.WriteLine("Please enter command as follows...");
            Console.WriteLine("SearchFoodTruck <searchKey> <Longitude> <Latitude>");
        }
    }
}
