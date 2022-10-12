"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/expensenotification").build();

connection.on("notification", function (notification){
    console.log(notification)
    var li = document.createElement("li")
    li.textContent = notification;
    document.getElementById("ExpenseNotificationList").appendChild(li);
})

connection.start().then( function () {
    var li = document.createElement("li")
    li.textContent = "No Notification Yet";
    document.getElementById("ExpenseNotificationList").appendChild(li);
   
})