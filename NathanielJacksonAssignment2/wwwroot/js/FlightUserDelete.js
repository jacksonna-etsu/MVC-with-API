"use strict";
//This function uses fetch to delete a specific passenger on a specific flight
//Information is passed to the api through form data
//Console information is logged to show that ajax has performed the operation
(function _flightUserDelete() {
    const formDeleteFlightRegistration = document.querySelector("#formDeleteFlightRegistration");
    formDeleteFlightRegistration.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/flightuserapi/removeflightregistration";
        const method = "delete";
        const formData = new FormData(formDeleteFlightRegistration);
        console.log(`${url} ${method}`);
        const flightId = formData.get("flightId");

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
                console.log("Flight passenger removed");
                window.location.replace(`/flight/details/${flightId}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();