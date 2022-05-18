"use strict";
//This function uses fetch to get information on all flight passengers
//All flight passengers across all flights are retrieved
//Console information is logged to show that ajax has performed the operation
(function _flightUserIndex() {
    const url = "/api/flightuserapi/flightregistrationreport/5";
    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('There was a network error!');
            }
            return response.json();
        })
        .then(result => {
            populateTable(result);
            console.log("Table populated")
        })
        .catch(error => {
            console.error('Error:', error);
        });
})();

//This function displays all passengers across all flights
//The list is generated one row at a time with a forEach loop
//Parameter: result - the data received from the API after the fetch call was made
function populateTable(result) {
    const tableBody = document.getElementById("tableBody");
    result.forEach((item) => {
        const tr = document.createElement("tr");
        for (let key in item) {
            const td = document.createElement("td");
            let text = item[key];
            let textNode = document.createTextNode(text);
            td.appendChild(textNode);
            tr.appendChild(td);
        }
        tableBody.appendChild(tr);
    });
}