"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = message + " - " + user;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("messageName").value;
    var AuthorID = document.getElementsByName("AuthorID")[0].value + "";
    var EventID = document.getElementsByName("EventID")[0].value + "";
    var UserName = document.getElementById("userName").value + "";

    connection.invoke("SendMessage", message, EventID, AuthorID, UserName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
