"use strict";
//This function uses fetch to update the review for a certain passenger of a flight
//Console information is logged to show that ajax has performed the operation
(function _flightUserUpdateFlightReview() {
    const formUpdateFlightReview = document.querySelector("#formUpdateFlightReview");
    formUpdateFlightReview.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/flightuserapi/changeflightreview";
        const method = "put";
        const formData = new FormData(formUpdateFlightReview);
        console.log(`${url} ${method}`);
        const flightId = formData.get("flightId");
        const flightReview = formData.get("Review");

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
                console.log(`Flight Review Changed ${flightReview}`);
                window.location.replace(`/flight/details/${flightId}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();