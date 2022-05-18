"use strict";
//This function uses fetch to get all reviews of a specific user
//Console information is logged to show that ajax has performed the operation
(function _flightUserViewUserReviews() {
    const url = "/api/flightuserapi/viewuserreviewsapi";
    const userIdForm = document.getElementById("userIdTransfer");
    const formData = new FormData(userIdForm);
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
            console.log("User reviews populated");
        })
        .catch(error => {
            console.error('Error:', error);
        });
})();

//This function displays all reviews for a specific user
//The list is generated one row at a time with a forEach loop
//Parameter: result - the data received from the API after the fetch call was made
function populateTable(result) {
    const tableBody = document.getElementById("tableBody");
    result.forEach((item) => {
        const tr = document.createElement("tr");
        for (let key in item) {
            const td = document.createElement("td");
            let text = item[key];
            console.log(key);
            let textNode = document.createTextNode(text);
            td.appendChild(textNode);
            tr.appendChild(td);
        }

        tableBody.appendChild(tr);
    });
}
