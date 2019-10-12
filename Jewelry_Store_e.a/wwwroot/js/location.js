
var BingMapsKey = 'AkDnTh-F1E565FfNPYCHbjs4JRMN6fC43_WivOhXMja2MVEFmDJhO2ZY8r_MuX54';

    function GetMap() {
        var map = new Microsoft.Maps.Map('#myMap', {
            credentials: 'AkDnTh-F1E565FfNPYCHbjs4JRMN6fC43_WivOhXMja2MVEFmDJhO2ZY8r_MuX54',
            center: new Microsoft.Maps.Location(32.07385, 34.7954)
        });
        /*var center = map.getCenter();

        var pin = new Microsoft.Maps.Pushpin(center, {
            title: '',
            subTitle: '',
            text: 'Tel Aviv'
        });
        map.entities.push(pin);*/
        //var query = ['Raoul Wallenberg 24, tel aviv', 'yamit 62, rishon le ziyon', 'Yigal Alon St 120, Tel Aviv-Yafo'];
        $.ajax({
            method: 'get',
            url: '/api/maps/address',
            success: (data) => {
                for (one in data) {
                    title = '';
                    subtitle = '';
                    txt = data[one].address;
                    var geocodeRequest = "https://dev.virtualearth.net/REST/v1/Locations?query=" + encodeURIComponent(txt) + "&key=" + BingMapsKey;
                    AddPushPin(map, geocodeRequest, title, subtitle, txt);
                }
            }
        }) 
}

function AddPushPin(map, request, title, subtitle, txt) {
    $.ajax({
        url: request,
        dataType: "jsonp",
        jsonp: "jsonp",
        success: function (r) {
            var location = r.resourceSets[0].resources;
            var pin1 = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(location[0].point.coordinates[0], location[0].point.coordinates[1]), {
                title: title,
                subTitle: subtitle,
                text: txt
            });
            map.entities.push(pin1);

        },
        error: function (e) {
            alert(e.statusText);
        }
    });
}

