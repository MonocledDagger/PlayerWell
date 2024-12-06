"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (username, IconPath, AuthorID, EventID, message, dateString) {
    var eventID = document.getElementsByName("EventID")[0].value + "";
    if (eventID == EventID) {
        //console.log("Receive method Javascript");

        const fragment = document.createDocumentFragment();
        var li = document.createElement("li");
        var CurrentUser = document.getElementById("PlayerID").value + "";

        //console.log("UsernameSender: " + username + "| AuthorIDSender: " + AuthorID + "| CurrentUser: " + CurrentUser);
        if (CurrentUser == AuthorID)
            CurrentUser = "sent";
        else
            CurrentUser = "received";
        li.classList.add("chat-message");
        li.classList.add(CurrentUser);


        if (CurrentUser == "received") //Adding profile image to message
        {
            var img = document.createElement("img");
            img.classList.add("avatar");
            img.alt = username + "\'s Avatar";
            img.src = "../images/" + IconPath;
            img.onclick = () => { on("../images/" + IconPath); };

            li.appendChild(img);
        }



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

        //var length = fragment.getElementsByTagName("li").length;
        //var li = fragment.getElementsByTagName("li")[length];
        //var div = li.getElementsByTagName("div")[0];

        div.appendChild(span);
        div.appendChild(p);
        div.appendChild(small);

        document.getElementById("messagesList").appendChild(fragment);  
    }
    
});
function on(picture) {
    console.log('On');
    document.getElementById("overlay").style.display = "block";
    document.getElementById("output").src = picture;
}
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    var EventID = document.getElementsByName("EventID")[0].value + "";
    var GroupName = "e" + EventID;
    connection.invoke("JoinGroup", GroupName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("comment").value + "";
    var AuthorID = document.getElementById("PlayerID").value + "";
    var EventID = document.getElementsByName("EventID")[0].value + "";
    var UserName = document.getElementById("userName").value + "";
    var IconPath = document.getElementById("iconPath").value + "";
    var GroupName = "e" + EventID;

    //console.log(message + " : " + AuthorID + " : " + EventID + " : " + UserName);
    connection.invoke("SendMessage", GroupName, message, EventID, AuthorID, UserName, IconPath).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
