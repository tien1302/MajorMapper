"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.start().then(function () {
    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("OnConnected", function () {
    OnConnected();
});

function OnConnected() {
    var consultantId = $('#hfUsername').val();
    connection.invoke("SaveUserConnection", consultantId).catch(function (err) {
        return console.error(err.toString());
    })
}

connection.on("ReceivedPersonalNotification", function (message, consultantId) {
    // alert(message + ' - ' + username);
    DisplayPersonalNotification(message, 'hey ' + consultantId);
});