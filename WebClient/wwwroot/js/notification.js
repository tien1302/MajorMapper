
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();


$(function () {
    connection.start().then(function () {
        console.log('connected to hub');
        InvokeNotifications();

    }).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("OnConnected", function () {
    OnConnected();
});

function OnConnected() {
    var consultantId = $('#hfUsername').val();
    connection.invoke("GetConnectionId", consultantId).catch(function (err) {
        return console.error(err.toString());
    })
}

//Get Notifications
function InvokeNotifications() {
    var consultantId = $('#hfUsername').val();
    connection.invoke("SendNotifications", consultantId).catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("ReceivedNotifications", function (products) {
    BindProductsToGrid(products);
});

function BindProductsToGrid(products) {
    $('#NotificationList').empty();
    
    $.each(products, function (index,product) {
        console.log(product);
        let li = $('<li/>');
        li.append($(`<a href="/Account/DetailsPlayer/${product.playerId}">`) // Create a link for each product
            .append($('<div class="notification-content">')
                
                .append($(`<h2>${product.title}</h2>`) )// Use product name as sender
                .append($(`<p>${product.notificationContent}</p>`)) // Display price in the message area
            )
        );
        $('#NotificationList').append(li);
    });
}


connection.on("ReceivedPersonalNotification", function (title, content) {
    debugger;
    DisplayPersonalNotification(title, content);
});