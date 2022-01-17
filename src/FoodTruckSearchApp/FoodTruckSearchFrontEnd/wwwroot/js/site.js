// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetMap() {
    var map = new Microsoft.Maps.Map('#myMap');

    //Load the spatial math module
    Microsoft.Maps.loadModule("Microsoft.Maps.SpatialMath", function () {
        //Request the user's location
        navigator.geolocation.getCurrentPosition(function (position) {
            var loc = new Microsoft.Maps.Location(position.coords.latitude, position.coords.longitude);

            //Create an accuracy circle
            var path = Microsoft.Maps.SpatialMath.getRegularPolygon(loc, position.coords.accuracy, 36, Microsoft.Maps.SpatialMath.Meters);
            var poly = new Microsoft.Maps.Polygon(path);
            map.entities.push(poly);

            //Add a pushpin at the user's location.
            var pin = new Microsoft.Maps.Pushpin(loc);
            map.entities.push(pin);

            //Center the map on the user's location.
            map.setView({ center: loc, zoom: 17 });
        });
    });
}

function RenderSearchResultsOnMap() {
    debugger;
   
    var searchTextParam = $('#txtSearchKey')[0].value;
    var latitudeParam = $('#txtSearchLatitude')[0].value;
    var longitudeParam = $('#txtSearchLongitude')[0].value;
    var url = 'https://localhost:44307/FoodTruckSearch/SearchFoodTruck?searchText=' + searchTextParam + '&latitude=' + latitudeParam + '&longitude=' + longitudeParam;
    $('#errorMessage')[0].innerHTML = "";
    $('#results')[0].innerHTML = "";
    var map = new Microsoft.Maps.Map('#myMap');
    map.entities.clear();

    fetch(url, { headers: { 'Content-Type': 'application/json; charset=UTF-8' } })
        .then(async (response) => {
           
            // get json response here
            let result = await response.json();
         
            alert(result.success);
            if (response.status === 200) {
                if (result.success) {
                    if (result.errorMessage == null) {
                        alert(result.success);
                        var loc = new Microsoft.Maps.Location(result.foodTruckDataList[0].latitude, result.foodTruckDataList[0].longitude);

                        //Center the map on the user's location.
                        map.setView({ center: loc, zoom: 10 });

                        for (let i = 0; i < result.foodTruckDataList.length; i++) {
                            alert(result.foodTruckDataList[i].foodItems);
                            $('#results')[0].innerHTML += "<br>" + result.foodTruckDataList[i].applicant + "---" + result.foodTruckDataList[i].foodItems;

                            var mapPoint = new Microsoft.Maps.Point(result.foodTruckDataList[i].x, result.foodTruckDataList[i].y);
                            //Add a pushpin at the user's location.
                            var pin = new Microsoft.Maps.Pushpin(mapPoint, { icon: 'https://www.bingmapsportal.com/Content/images/poi_custom.png', anchor: new Microsoft.Maps.Point(12, 28) }, { text: 'Food truck', title: result.foodTruckDataList[i].applicant, subTitle: result.foodTruckDataList[i].foodItems });
                            map.entities.push(pin);

                        }
                    }
                    else {
                        $('#errorMessage')[0].innerHTML = result.errorMessage;
                    }

                    
                } else {
                    alert(result.errorMessage);
                    $('#errorMessage')[0].innerHTML = result.errorMessage;
                }
            }
            else {
                $('#errorMessage')[0].innerHTML = "Error occured while executing the search. Please try again later or contact support if problem persists."
            }

        })
        .catch(err => console.log(err))
}



