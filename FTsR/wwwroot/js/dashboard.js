"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
	connection.start().then(function () {

		InvokeProducts();
		InvokeCompanies();

	}).catch(function (err) {
		return console.error(err.toString());
	});
});


connection.on("ReceiveMessage", function (message) {
	alert(message);
});

function Test(message) {
	connection.invoke("Say", message).catch(function (err) {
		return console.error(err.toString());
	});
}

function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString());
	});
}

function InvokeCompanies() {
	connection.invoke("SendCompanies").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedProducts", function (products) {
	BindProductsToGrid(products);
});

function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty();

	var tr;
	$.each(products, function (index, product) {
		tr = $('<tr/>');
		tr.append(`<td>${(index + 1)}</td>`);
		tr.append(`<td>${product.name}</td>`);
		tr.append(`<td>${product.category}</td>`);
		tr.append(`<td>${product.price}</td>`);
		$('#tblProduct').append(tr);
	});
}

connection.on("ReceivedCompanies", function (companies) {
	BindCompaniesToGrid(companies);
});


function BindCompaniesToGrid(companies) {
	$('#tblCompanies tbody').empty();

	var tr;
	$.each(companies, function (index, company) {
		tr = $('<tr/>');
		tr.append(`<td>${(index + 1)}</td>`);
		tr.append(`<td>${company.name}</td>`);
		tr.append(`<td>${company.type}</td>`);
		tr.append(`<td>${company.branch}</td>`);
		$('#tblCompanies').append(tr);
	});
}
