using FoodTruckBackendDataModel;
using Newtonsoft.Json;
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
                try
                {
                    if (!string.IsNullOrEmpty(args[0]) && args.Length == 3)
                    {
                        if (args[0] == "-h")
                        {
                            SearchHelpText();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(args[0].ToString()))
                            {
                                searchKey = args[0].ToString();
                            }

                            if (!string.IsNullOrEmpty(args[1].ToString()))
                            {
                                latitude = args[1].ToString();
                            }

                            if (!string.IsNullOrEmpty(args[2].ToString()))
                            {
                                longitude = args[2].ToString();
                            }

                            Console.WriteLine($"Searching food trucks for {searchKey}, Latitude - {latitude}, Longitude - {longitude}");
                            
                            JObject response = Task.Run(() => FoodTruckSearchCLI.SearchFoodTruck(searchKey, latitude, longitude)).Result;
                            var foodTruckResponse = JsonConvert.DeserializeObject<FoodTruckResponse>(response.ToString());

                            if (foodTruckResponse.Success)
                            {
                                if (!string.IsNullOrEmpty(foodTruckResponse.ErrorMessage))
                                {
                                    Console.WriteLine($"These are the food trucks available based on your search:-");

                                    foreach (FoodTruckData item in foodTruckResponse.FoodTruckDataList)
                                    {
                                        //TODO:Its probably better to return the address as well here.
                                        Console.WriteLine($"{item.Applicant} --- {item.FoodItems} -- {item.Latitude} -- {item.Longitude}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"{foodTruckResponse.ErrorMessage}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Error occurred searching the food trucks. Error - {foodTruckResponse.ErrorMessage}");
                            }


                        }

                    }
                    else
                    {
                        SearchHelpText();
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error occurred searching the food trucks. Error - {ex.Message}");
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
