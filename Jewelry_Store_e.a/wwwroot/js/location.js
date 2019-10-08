
var BingMapsKey = 'AkDnTh-F1E565FfNPYCHbjs4JRMN6fC43_WivOhXMja2MVEFmDJhO2ZY8r_MuX54';

function geocode(address) {
    //query = 'Dikla 20, rishon le zion';
    query = address;
    //document.getElementById('input').value;
    var geocodeRequest = "https://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURIComponent(query) + "&key=" + BingMapsKey;

    return CallRestService(geocodeRequest, GeocodeCallback);
}

function GeocodeCallback(response) {
    var output = document.getElementById('output');

    if (response &&
        response.resourceSets &&
        response.resourceSets.length > 0 &&
        response.resourceSets[0].resources) {

        var results = response.resourceSets[0].resources;
        return results;
        //GetMap(results);
        }
}

    function CallRestService(request, callback) {
        $.ajax({
            url: request,
            dataType: "jsonp",
            jsonp: "jsonp",
            success: function (r) {
                 return(callback(r));
            },
            error: function (e) {
                alert(e.statusText);
            }
        });
}
/*function NewMap() {
    //var location = geocode('Dikla 20, rishon le zion');
    new Microsoft.Maps.Map('#myMap', {
        credentials: 'AkDnTh-F1E565FfNPYCHbjs4JRMN6fC43_WivOhXMja2MVEFmDJhO2ZY8r_MuX54',
        
        center: new Microsoft.Maps.Location(31.9784, 34.76198)
        //(31.9784, 34.76198)
    });
}*/
    function GetMap() {
        //var location = geocode('Dikla 20, rishon le zion');
        var map = new Microsoft.Maps.Map('#myMap', {
            credentials: 'AkDnTh-F1E565FfNPYCHbjs4JRMN6fC43_WivOhXMja2MVEFmDJhO2ZY8r_MuX54',
            center: new Microsoft.Maps.Location(31.9784, 34.76198)
            //
        });
        var center = map.getCenter();
        //var map = Microsoft.Maps.GetMap('#myMap');
        //Create custom Pushpin
        var pin = new Microsoft.Maps.Pushpin(center, {
            title: 'Rishon',
            subTitle: 'Dikla',
            text: '20'
        });
        //32.11114	34.83894
        var location = geocode('Raoul Wallenberg 24, tel aviv');
        var pin1 = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(new Microsoft.Maps.Location(location[0].point.coordinates[0], location[0].point.coordinates[1])), {
            title: 'Oranit',
            subTitle: 'Hadas',
            text: '3'
        });
        

        //Add the pushpin to the map
        map.entities.push(pin);
        map.entities.push(pin1);
        
    }
