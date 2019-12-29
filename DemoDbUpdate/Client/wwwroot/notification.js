"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.on("datachanged", function () {
    console.log("Javascript received a datachanged message from the SignalR Server");
    DotNet.invokeMethodAsync('DemoDbUpdate.Client', 'DataChanged');
});

connection.start().then(function () {
    console.log("Signalr connection started");
}).catch(function (err) {
    return console.error(err.toString());
});