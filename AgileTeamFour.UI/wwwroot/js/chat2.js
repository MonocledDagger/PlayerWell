"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (username, AuthorID, message, dateString) {

    console.log("Receive method Javascript");
    
    const fragment = document.createDocumentFragment();
    var li = document.createElement("li");
    var CurrentUser = document.getElementsByName("PlayerID")[0].value + "";

    console.log("UsernameSender: " + username + "| AuthorIDSender: " + AuthorID + "| CurrentUser: " + CurrentUser);
    if (CurrentUser == AuthorID)
        CurrentUser = "sent";
    else
        CurrentUser = "received";
    li.classList.add("chat-message");
    li.classList.add(CurrentUser);

    var div = document.createElement("div")
    div.classList.add("message-content");
    fragment.appendChild(li).appendChild(div);

    var span = document.createElement("span");
    span.classList.add("message-author");
    span.innerText = username;

    var p = document.createElement("p");
    p.innerText = message;

    var small = document.createElement("small")
    small.classList.add("message-timestamp");
    small.innerText = dateString;



    div.appendChild(span);
    div.appendChild(p);
    div.appendChild(small);

    document.getElementById("messagesList").appendChild(fragment);  
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("comment").value + "";
    var AuthorID = document.getElementsByName("PlayerID")[0].value + "";
    var GuildId = document.getElementsByName("GuildId")[0].value + "";
    var UserName = document.getElementById("userName").value + "";
    
    
    connection.invoke("SendMessage", message, GuildId, AuthorID, UserName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
