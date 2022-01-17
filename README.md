# FoodTruckSearchSA
This repository contains the source code used for setting up a web app to search food trucks in San Francisco area. It uses ASP.Net MVC Core web app and web api to search the open data available for food truck info in San Francisco. It uses bing map control to provide a visual representation of the search results. It uses SODA Net nuget packages to send search request calls to soda database.

[![Docker Image CI](https://github.com/jjoy83/FoodTruckSearchSA/actions/workflows/docker-image.yml/badge.svg?branch=main)](https://github.com/jjoy83/FoodTruckSearchSA/actions/workflows/docker-image.yml)


## Getting Started
1. Download the entire source code to a local folder. 
2. Add the Soda account token in the appsetting.json file for BackendAPI
3. Run the powershell command build-backend.ps1
4. This will host the backend web api at the url https://localhost:5000. Open the url to access the swagger pages.
5. Add the bingMapKey in the Layout.html file in the script section
6. Run the powershell command build-frontend.ps1.This will host the frontend web app at the url https://localhost:5002. Open the url to access the web site.
7. Search some data such as taco, "latitude": "37.790485146128",   "longitude": "-122.40094044068951",

![image](https://user-images.githubusercontent.com/35704508/149718447-fcbfd84a-46da-491e-a0aa-ed393a60058f.png)

![image](https://user-images.githubusercontent.com/35704508/149812127-18d765a2-df8a-44cc-912a-09169a1c088c.png)


## Project Structure
These are the main components of the project

- FoodTruckSearchBackendAPI - this is the asp.net mvc core web api to call the soda search client to get search results.
- FoodTruckSearchFrontend - this is the asp.net mvc core web app front end website.
- FoodTruckConsoleApp - this is the console application to provide CLI interface. This is c# console application.
- FoodTruckSearchSODAClient - this is the c# class library to connect to soda client to run searches. It uses SODA Net nuget package
- FoodTruckBackendDataModel - this contains the data model passed to api for request and response.
- HttpClientWrapper - this is a wrapper class for HTTPClient requests. This is not used currently
- FoodTruckBingMapClient - this is to call bing maps api to get the nearest locations. This is not used anymore since soda library provide a mechanism to search near by locations.

All components have the unit test classes.

## Running the project
There are 2 ways to run this project locally for debugging. The backend urls will be different based on which approach is being used. The backend url can be set in the appsetting.json file for FrontEnd web app or app.config file for console app.
- You can use Visual Studio to build and run the frontend and backend api using IISExpress. 
- You can use the docker files for frontend and backend to run it using docker desktop
- Provide the bingMapKey in the layout.html file
- Provide the soda account app token in the appSettings.json file.
- There is a console app for CLI use. Run the FoodTruckSearchConsoleApp.exe by providing the parameters for searchText, Latitude and Longitude.

## Deploying the app
For production deployment, the best option is to use the docker images, push it to container registry in Azure or github and then deploy it to Azure web apps. Alternatively based on the performance or scalability requirement, it can be deployed to Kubernetes cluster as well. The deployment workflow is added in GitHub actions. Once all the appropriate values are filled, it can be enabled.


## Pending Items 
Due to lack of time the following items are not implemented
- Use Azure Cognitive search to perform search with open data
- Use Azure keyvaults to store the secrets in appsetting.Json file
- Enable authentication for BackendAPI using Microsoft identity server
- More unit tests to cover positive and negative cases. Missing unit test for soda client using Moq. Hookup code coverage and run sonar qube code analysis.
- Since no controller methods are added, no unit tests are added for controllers
- Improve the UI experience by using bootstrap or better controls 
- Integration tests for backend api
- Security scans
- Initial plan was to use bing map location api to get all the nearest addresses from the latitude and longitude provided and then use that data to find the food trucks. But later on realized that soda query provides within_circle soql option to return the near by locations. The bing map client is used as part of the initial testing. Moq was used for testing BingMapClinet







