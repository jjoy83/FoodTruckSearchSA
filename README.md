# FoodTruckSearchSA
This repository contains the source code used for setting up a web app to search food trucks in San Francisco area. It uses ASP.Net MVC Core web app and web api to search the open data available for food truck info in San Francisco. It uses bing map control to provide a visual representation of the search results. It uses SODA Net nuget packages to send search request calls to soda database.

[![Docker Image CI](https://github.com/jjoy83/FoodTruckSearchSA/actions/workflows/docker-image.yml/badge.svg?branch=main)](https://github.com/jjoy83/FoodTruckSearchSA/actions/workflows/docker-image.yml)


## Getting Started
1. Download the entire source code to a local folder. 
2. Run the powershell command build-backend.ps1
3. This will host the backend web api at the url https://localhost:5000. Open the url to access the swagger pages.
4. Run the powershell command build-frontend.ps1.This will host the frontend web app at the url https://localhost:5002. Open the url to access the web site.
5. Search some data such as taco, "latitude": "37.790485146128",   "longitude": "-122.40094044068951",

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
There are 2 ways to run this project locally for debugging. 
- You can use Visual Studio to build and run the frontend and backend api using IISExpress
- You can use the docker files for frontend and backend to run it using docker desktop

## Deploying the app




