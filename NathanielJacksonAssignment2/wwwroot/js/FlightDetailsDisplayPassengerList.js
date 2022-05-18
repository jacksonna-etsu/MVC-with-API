"use strict";
//This function uses fetch to get information on all passengers
//On a flight. flightId is passed through fetch in form data
//The api function uses this to determine what flight to
//Display passengers for
(function _flightDetailsDisplayPassengerList() {
    const url = "/api/flightuserapi/getpassengersforflight";
    const flightIdForm = document.getElementById("flightIdTransfer");
    const formData = new FormData(flightIdForm);
    fetch(url, {
        method: "post",
        body: formData
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('There was a network error!');
            }
            return response.json();
        })
        .then(result => {
            populateTable(result);
            console.log("Passenger list populated");
        })
        .catch(error => {
            console.error('Error:', error);
        });
})();

//This function displays all passengers on the current flight
//Each passenger's attributes are displayed with a list
//Of links that the user can click to modify that passenger
//The list is generated one row at a time with a forEach loop
//Parameter: result - the data received from the API after the fetch call was made
function populateTable(result) {
    const tableBody = document.getElementById("tableBody");
    result.forEach((item) => {
        const tr = document.createElement("tr");
        const aReview = document.createElement("a");
        const aClass = document.createElement("a");
        const aRemove = document.createElement("a");


        const tdUserName = document.createElement("td");
        let textUserName = item["passengerName"];
        let textNodeUserName = document.createTextNode(textUserName);
        tdUserName.appendChild(textNodeUserName);

        const tdFlightCost = document.createElement("td");
        let textFlightCost = item["flightCost"];
        let textNodeFlightCost = document.createTextNode(textFlightCost);
        tdFlightCost.appendChild(textNodeFlightCost);

        const tdFlightClass = document.createElement("td");
        let textFlightClass = item["flightClass"];
        let textNodeFlightClass = document.createTextNode(textFlightClass);
        tdFlightClass.appendChild(textNodeFlightClass);

        tr.appendChild(tdUserName);
        tr.appendChild(tdFlightCost);
        tr.appendChild(tdFlightClass);

        aReview.setAttribute("href", `/flightuser/updateflightreview?flightId=${item["flightId"]}&userId=${item["userId"]}`);
        aReview.textContent = "Write Review";
        tr.appendChild(aReview);

        aClass.setAttribute("href", `/flightuser/updateflightclass?flightId=${item["flightId"]}&userId=${item["userId"]}`);
        aClass.textContent = "Change Flight Class";
        tr.appendChild(aClass);

        aRemove.setAttribute("href", `/flightuser/remove?flightId=${item["flightId"]}&userId=${item["userId"]}`);
        aRemove.textContent = "Remove User";
        tr.appendChild(aRemove);

        tableBody.appendChild(tr);
    });
}
