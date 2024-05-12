"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dataHub").build();

connection.start().then(() => {
    console.log("Connected to hub");
}).catch(err => console.error(err));

connection.on("ReceiveData", function (name, value, price) {
    console.log(`Received data: ${ name } - ${ value } - ${price}`);
});

function sendData(name, value, price) {
    connection.invoke("UpdateData", name, value, price).catch(err => console.error(err));
}
