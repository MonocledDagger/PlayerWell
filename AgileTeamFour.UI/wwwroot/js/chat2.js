"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub2").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage2", function (username, IconPath, AuthorID, guildID, message, dateString) {

    var GuildId = document.getElementsByName("GuildId")[0].value + "";
    if (GuildId == guildID)
    {
        //console.log("Receive method Javascript");

        const fragment = document.createDocumentFragment();
        var li = document.createElement("li");
        var CurrentUser = document.getElementsByName("PlayerID")[0].value + "";

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

        div.appendChild(span);
        div.appendChild(p);
        div.appendChild(small);

        document.getElementById("messagesList").appendChild(fragment);  
    }

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    var GuildId = document.getElementsByName("GuildId")[0].value + "";
    var GroupName = "g" + GuildId;
    connection.invoke("JoinGroup", GroupName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("comment").value + "";
    var AuthorID = document.getElementsByName("PlayerID")[0].value + "";
    var GuildId = document.getElementsByName("GuildId")[0].value + "";
    var UserName = document.getElementById("userName").value + "";
    var IconPath = document.getElementById("iconPath").value + "";
    var GroupName = "g" + GuildId;
    //Clear out inputted text after sending a message
    document.getElementById("comment").value = "";
    connection.invoke("SendMessage2", GroupName, message, GuildId, AuthorID, UserName, IconPath).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
