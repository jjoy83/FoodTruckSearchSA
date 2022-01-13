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

function Search()
{
    
}
