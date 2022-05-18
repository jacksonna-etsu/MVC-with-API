"use strict";
//This function uses fetch to update the flight class for a specific passenger
//The new flight class is passed as form data
//Console information is logged to show that ajax has performed the operation
(function _flightUserUpdateFlightClass() {
    const formUpdateFlightClass = document.querySelector("#formUpdateFlightClass");
    formUpdateFlightClass.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/flightuserapi/changeflightclass";
        const method = "put";
        const formData = new FormData(formUpdateFlightClass);
        console.log(`${url} ${method}`);
        const flightId = formData.get("flightId");
        const flightClass = formData.get("FlightClass");

        fetch(url, {
            method: method,
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('There was a network error!');
                }
                return response.status;
            })
            .then(result => {
                console.log(result);
                console.log(`Flight Class Changed ${flightClass}`);
                window.location.replace(`/flight/details/${flightId}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();