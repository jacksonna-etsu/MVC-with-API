"use strict";
//This function uses fetch to get all users in the system
//Console information is logged to show that ajax has performed the operation
(function _flightUserViewUsers() {
    const url = "/api/flightuserapi/viewusersapi";
    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('There was a network error!');
            }
            return response.json();
        })
        .then(result => {
            populateTable(result);
            console.log("List of users populated");
        })
        .catch(error => {
            console.error('Error:', error);
        });
})();

//This function displays all users in the system
//Each user is displayed with a link to reference that user's reviews
//The list is generated one row at a time with a forEach loop
//Parameter: result - the data received from the API after the fetch call was made
function populateTable(result) {
    const tableBody = document.getElementById("tableBody");
    result.forEach((item) => {
        const tr = document.createElement("tr");
        const a = document.createElement("a");
        for (let key in item) {
            const td = document.createElement("td");
            let text = item[key];
            console.log(key);
            let textNode = document.createTextNode(text);
            td.appendChild(textNode);
            tr.appendChild(td);
        }
        a.setAttribute("href", `/FlightUser/ViewUserReviews?userId=${item["userId"]}`);
        a.textContent = "Reviews";
        tr.appendChild(a);
        tableBody.appendChild(tr);
    });
}
