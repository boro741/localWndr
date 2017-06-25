
/*Get Latitute and Longitude of current location*/
var currLat;
var currLng;
var shops = [
    {lat: 17.439930,lng: 78.498276},
    {lat: 17.463053 ,lng: 78.366825},
    {lat: 17.443414,lng: 78.387842},
    {lat: 17.436678,lng: 78.386554}];

var shopsNearRad = [];
var nearMe = [];


var x = document.getElementById("demo");
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else { 
        x.innerHTML = "Geolocation is not supported by this browser.";
    } 
}

function showPosition(position) {
    currLat = (position.coords.latitude);
    currLng = (position.coords.longitude);
    x.innerHTML = "Latitude: " + position.coords.latitude + 
    "<br>Longitude: " + position.coords.longitude;
    
    shopNearMe(currLat,currLng,5.0);
}

//console.log(currLat);
/**/


function shopNearMe(currLat,currLng,radius){
    
    for(var i=0;i<shops.length;i++){
        
        shopsNearRad[i] = distFrom(currLat,currLng,shops[i].lat,shops[i].lng);
        
         if(shopsNearRad[i] < radius){
                nearMe.push(shopsNearRad[i]);
         }
    }
    
   console.log("shopsNearRad: "+ shopsNearRad);
   console.log("nearMe: "+ nearMe);
}

Math.radians = function(degrees) {
  return degrees * Math.PI / 180;
};


function distFrom(lat1,lng1,lat2,lng2) {
    
    var earthRadius = 6371.0; // miles 3958.75 (or 6371.0 kilometers)
    var dLat = Math.radians(lat2-lat1);
    var dLng = Math.radians(lng2-lng1);
    var sindLat = Math.sin(dLat / 2);
    var sindLng = Math.sin(dLng / 2);
    var a = Math.pow(sindLat, 2) + Math.pow(sindLng, 2)
            * Math.cos(Math.radians(lat1)) * Math.cos(Math.radians(lat2));
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    var dist = earthRadius * c;

    
    //console.log("dist: "+dist);
    return dist;
    }

//var R = distFrom()