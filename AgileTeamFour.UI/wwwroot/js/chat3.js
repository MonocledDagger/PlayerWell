"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
let groups = new Array();
//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessageFriend", function (username, IconPath, AuthorID, FriendSentToID, message, dateString) {
    var authorID = document.getElementById("PlayerID").value + "";
    var friendID = document.getElementById("friends").value + "";

    if ((authorID == AuthorID && friendID == FriendSentToID) || (authorID == FriendSentToID && friendID == AuthorID))
    {
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
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("comment").value + "";
    var AuthorID = document.getElementById("PlayerID").value + "";
    var FriendID = document.getElementById("friends").value + "";
    var UserName = document.getElementById("userName").value + "";
    var IconPath = document.getElementById("IconPath").value + "";
    var GroupName;
    if (AuthorID > FriendID) {
        var GroupName = "f" + AuthorID + FriendID;
    }
    else {
        var GroupName = "f" + FriendID + AuthorID;
    }
    connection.invoke("SendMessageFriend", GroupName, message, FriendID, AuthorID, UserName, IconPath).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
document.getElementById("friends").addEventListener("change", function (event) {
    if (document.getElementById("friends").value != "Empty" && document.getElementById("friends").value != "") {

        var AuthorID = document.getElementById("PlayerID").value + "";
        var FriendID = document.getElementById("friends").value + "";

        var messageList = document.getElementById("messagesList");

        if (messageList.firstChild != null)
        {
            while (messageList.firstChild) {
                messageList.removeChild(messageList.firstChild);
            }
        }

        var length = groups.length;
        for (let i = 0; i < length; i++)
        {
            connection.invoke("LeaveGroup", groups.pop()).catch(function (err) {
                return console.error(err.toString());
            });            
        }

        if (AuthorID > FriendID) {
            var GroupName = "f" + AuthorID + FriendID;
            groups.push(GroupName);
        }
        else {
            var GroupName = "f" + FriendID + AuthorID;
            groups.push(GroupName);
        }

        connection.invoke("JoinGroup", GroupName).catch(function (err) {
            return console.error(err.toString());
        });  
        connection.invoke("GetAllFriendMessages", FriendID, AuthorID).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
});


