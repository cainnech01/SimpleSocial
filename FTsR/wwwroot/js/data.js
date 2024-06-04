"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dataHub").build();

connection.start().then(() => {
    console.log("Connected to hub");
    InvokePins();
}).catch(err => console.error(err));

function InvokePins() {
    connection.invoke("SendPins").catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("ReceiveData", function (name, value, price) {
    console.log(`Received data: ${ name } - ${ value } - ${price}`);
});

connection.on("ReceivedPins", function (pins) {
    UpdatePins(pins);
});

function UpdatePins(pins) {
    $('#pins_row').empty();
    $.each(pins, function (index,pins) {
        let card = document.createElement('div');
        card.setAttribute('class', 'card border-0 mx-3');
        card.setAttribute('style', 'width: 18rem;');
        let a = document.createElement('a');
        a.setAttribute('href', `Pin/Index?id=${pins.id}`);
        let cBody = document.createElement('div');
        cBody.setAttribute('class', 'card-body');
        let h5 = document.createElement('h5');
        h5.setAttribute('class', 'card-title');
        h5.textContent = `${pins.title}`;
        let p = document.createElement('p');
        p.textContent = `${pins.author}`;
        p.setAttribute('class', 'card-text');
        let img = document.createElement('img');
        img.setAttribute('class', 'rounded-circle');
        img.setAttribute('height', '25');
        img.setAttribute('width', '25');
        img.setAttribute('src', `data:image/jpeg;base64, ${pins.authorProfileImage}`);
        let imgMain = document.createElement('img');
        imgMain.setAttribute('class', 'card-img-top');
        imgMain.setAttribute('src', `data:image/jpeg;base64, ${pins.image}`);
        imgMain.setAttribute('id', 'index_img');
        $(a).append($(imgMain));
        $(p).append($(img));
        $(cBody).append($(h5), $(p), $(img));
        $(card).append($(a), $(cBody));
        $('#pins_row').append($(card));
    })
}

function sendData(name, value, price) {
    connection.invoke("UpdateData", name, value, price).catch(err => console.error(err));
}
