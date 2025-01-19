// Declare addressesArray as a global variable
let userArray;
let openInfoWindow = null;
let userlat;
let userlng;
// Function to initialize map with markers
var mapOptions = {
    mapId: "DEMO_MAP_ID",
    center: { lat: userlat, lng: userlng }, // Boston coordinates
    zoom: 14
}


// Define the function to fetch data and initialize map
window.initializeMap = function (useraddress) {
    const requestOptions = {
        method: "GET",
        redirect: "follow"
    };

    console.log("The passed in address is" + useraddress.toString());

    const apiKey = "AIzaSyAoC-Zf3tvESU2_hGMaTeBUDeZAQd0QA8o";
    const apiUrl = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(useraddress)}&key=${apiKey}`;
    fetch(apiUrl)
        .then(response => response.json())
        .then(userdata => {
            // Extract latitude and longitude from the response
            const userlocation = userdata.results[0].geometry.location;
            userlat = parseFloat(userlocation.lat);
            userlng = parseFloat(userlocation.lng);
            console.log("user lat is " + userlat);
            console.log("user long is " + userlng);
        });

    fetch("https://localhost:7128/api/Pet", requestOptions)
        .then(response => response.json()) // Convert the response to JSON
        .then(result => {
            console.log(result);
            //// Assuming the response is an array
            //addressesArray = result.map(item => item.address);

            userArray = result;

            mapOptions.center = { lat: userlat, lng: userlng };

            // Proceed with map initialization
            initializeMapWithMarkers();
        })
        .catch(error => console.error('Error:', error));
};

function initializeMapWithMarkers() {
    const map = new google.maps.Map(document.getElementById('map'), mapOptions);
    const date = new Date();

    userArray.forEach((user, index) => {
        const userDate = new Date(user.startDate);
        if (user.status == "Created" && userDate >= date) {
            console.log(user);
            address = user.address;

            // Make the markers images (can't seem to get the sizes to normalize)
            const reservationImage = document.createElement("img");
            reservationImage.src = `${user.photoUrl}`
            reservationImage.style.width = "50px";
            reservationImage.style.height = "50px";
            reservationImage.style.objectFit = "cover"; // Ensure the entire image is visible within the dimensions
            reservationImage.style.clipPath = "circle(50% at 50% 50%)";

            const apiKey = "AIzaSyAoC-Zf3tvESU2_hGMaTeBUDeZAQd0QA8o";
            const apiUrl = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(address)}&key=${apiKey}`;
            fetch(apiUrl)
                .then(response => response.json())
                .then(data => {
                    // Extract latitude and longitude from the response
                    const location = data.results[0].geometry.location;
                    var lat = location.lat;
                    var lng = location.lng;

                    // Slightly randomize location, mainly so the markers don't overlap, but also security...
                    lat += (Math.random() - 0.1) * 0.01;
                    lng += (Math.random() - 0.1) * 0.01;
                    console.log({ lat, lng })

                    // plot the markers on the created map
                    var marker = new google.maps.marker.AdvancedMarkerElement({
                        position: { lat: lat, lng: lng },
                        map: map,
                        content: reservationImage,
                        title: 'Marker ' + (index + 1)
                    });

                    var startdate = user.startDate.split("T")[0];
                    var [year, month, day] = startdate.split("-");
                    var start = `${month}/${day}/${year}`;

                    var enddate = user.endDate.split("T")[0];
                    var [year, month, day] = enddate.split("-");
                    var end = `${month}/${day}/${year}`;

                    marker.addListener("click", ({ domEvent, latLng }) => {
                        const { target } = domEvent;

                        if (openInfoWindow !== null) {
                            //console.log("Closing windows");
                            openInfoWindow.close();
                        }
                        const infowindow = new google.maps.InfoWindow({
                            content: `<div>${user.name} the ${user.species}</div> <div>${start} - ${end}</div>`
                        });
                        infowindow.open(map, marker);
                        openInfoWindow = infowindow;
                        //console.log("Setting openInfoWindow bool to " + openInfoWindow);
                        var id = user.id;
                        var name = user.username;
                        //console.log("marker clicked");
                        DotNet.invokeMethodAsync('PetMateCoreHosted.Client', 'Panel', id, name);

                    });
                })
                .catch(error => console.error("Error:", error));
        }
    });
}

window.scrollToBottom = function () {
    var element = document.getElementById("messagesContainer");
    element.scrollTop = element.scrollHeight;
}
