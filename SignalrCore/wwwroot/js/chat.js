var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//登入
connection.on("online", function (user) {
    var encodedMsg = ShowTime() + user
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveMessage", function (user, message) {
    var encodedMsg = ShowTime() + user + "說:" + message
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveMessageOne", function (user, msg) {
    var encodedMsg = ShowTime() + user + ":" + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var sampleValue = document.getElementById("users").value;
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    if (sampleValue == "") {
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
    } else {
        connection.invoke("SendOneMessage", sampleValue, user, message).catch(function (err) {
            return console.error(err.toString());
        });
    }

    event.preventDefault();
});

document.getElementById("sendButton1").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessageByServer", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function ShowTime() {
    var TimeNow = new Date();
    var h = (TimeNow.getHours() < 10 ? '0' : '') + TimeNow.getHours();
    var m = (TimeNow.getMinutes() < 10 ? '0' : '') + TimeNow.getMinutes();
    var s = (TimeNow.getSeconds() < 10 ? '0' : '') + TimeNow.getSeconds();

    return h + ':' + m + ':' + s + ':';
}