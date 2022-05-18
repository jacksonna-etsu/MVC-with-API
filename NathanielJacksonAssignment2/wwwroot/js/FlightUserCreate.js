"use strict";
//This function uses fetch to create a new passenger for a specific flight
//information is passed to the api through form data
//Console information is logged to show that ajax has performed the operation
(function _flightUserCreate() {
    const formCreateFlightUser = document.querySelector("#formCreateFlightUser");
    formCreateFlightUser.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/flightuserapi/createflightregistration";
        const method = "post";
        const formData = new FormData(formCreateFlightUser);
        const flightId = formData.get("flightId");
        const userId = formData.get("userId");
        console.log(`${url} ${method}`);

        fetch(url, {
            method: method,
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('There was a network error!');
                }
                return response.json();
            })
            .then(result => {
                console.log(`user with id:${userId} added to flight:${flightId}`);
                window.location.replace(`/flight/details/${flightId}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();