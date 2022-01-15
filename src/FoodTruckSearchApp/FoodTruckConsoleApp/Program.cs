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
            Console.WriteLine("Welcome to search food trucks.");
            string searchKey = string.Empty;
            string latitude = string.Empty;
            string longitude = string.Empty;

            if (args != null)
            {
                if (!string.IsNullOrEmpty(args[0]) && args.Length == 3)
                {
                    if (args[0] == "-h")
                    {
                        SearchHelpText();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(args[0]))
                        {
                            searchKey = args[0];
                        }

                        if (!string.IsNullOrEmpty(args[1]))
                        {
                            longitude = args[1];
                        }

                        if (!string.IsNullOrEmpty(args[2]))
                        {
                            latitude = args[2];
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
            Console.WriteLine("Please enter parameters as follows...");
            Console.WriteLine("<searchKey> <Longitude> <Latitude>");
        }
    }
}
