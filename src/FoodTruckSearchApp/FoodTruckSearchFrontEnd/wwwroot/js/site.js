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

function RenderSearchResultsOnMap()
{
    debugger;
    var base64Image = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAAcBAMAAABmCgnjAAAAFVBMVEVHcEz///8aGhpsuy2s4oDRuPMaADiElDMGAAAAAXRSTlMAQObYZgAAAFNJREFUeF7FkLENgDAQA52GGiIxwD8bPBsgBmD/aXCs6JMNcpWtk1wYx8SO6gO2N3kWtDuJBc39UtYTPEc69BIpTVqLgjJOpS7tQ1I5NyiGia2rHyT7OTg7xhBoAAAAAElFTkSuQmCC';
    var map = new Microsoft.Maps.Map('#myMap');

    var searchTextParam = document.getElementsByName('searchText')[0].value;
    var latitudeParam = document.getElementsByName('latitude')[0].value;
    var longitudeParam = document.getElementsByName('longitude')[0].value;

    fetch('https://localhost:44307/FoodTruckSearch/SearchFoodTruck?searchText=' + searchTextParam + '&latitude=' + latitudeParam + '&longitude=' + longitudeParam)
        .then(async (response) => {

            // get json response here
            let data = await response.json();
            
            if (response.status === 200) {
                alert(data);
                var result = Json.parse(data);
                for (item in result.FoodTruckDataList) {
                    var mapPoint = new Microsoft.Maps.Point(item.x, item.y);
                    //Add a pushpin at the user's location.
                    var pin = new Microsoft.Maps.Pushpin(mapPoint, { icon: base64Image, anchor: new Microsoft.Maps.Point(12, 28) }, { text: 'Food truck', title: item.Applicant, subTitle: item.FoodItems });
                    map.entities.push(pin);

                }
            } else {
                document.getElementsByName('searchText')[0].innerHTML = "Error occured while executing the search. Please try again later or contact support if problem persists."
            }

        })
        .catch((err) => {
            console.log(err);
        })
}
