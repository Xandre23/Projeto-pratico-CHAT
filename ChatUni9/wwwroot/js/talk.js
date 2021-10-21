"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();
$("#send").disabled = true;

connection.on("ReceiveMessage", function (user, message) {

    console.log("menssage ", message);
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var li = $("<li></li>").text(user + ": " + msg);
    li.addClass("list-group-item");
    $("#messagesList").append(li);
});

connection.start().then(function () {
    $("#send").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

$("#send").on("click", function (event) {
    var user = "leonardo@gmail.com";
    var message = $("#txt-menssage").val();

    makeHTMLMessageSent(getHour(), getDate(), message);

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function getDate() {
    const date = new Date();
    return `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
}

function getHour() {
    const options = {
        timeZone: 'America/Sao_Paulo',
        hour: 'numeric',
        minute: 'numeric'
    };
    return new Intl.DateTimeFormat([], options);
}

function makeHTMLMessageSent(hour, formattedDate, message) {
    const spanMessageDateTime = jQuery("<span>", {
        "class": "message-data-time",
        "text": `${hour.format(new Date())} - ${formattedDate}`
    });

    const divMessageData = jQuery("<div>", {
        "class": "message-data text-right"
    }).append(spanMessageDateTime);

    const divMessage = jQuery("<div>", {
        "class": "message other-message float-right",
        "text": message
    });

    const li = jQuery("<li>", {
        "class": "clearfix"
    }).append(divMessageData, divMessage);

    $("ul.chat-list-messages").append(li);
}

$(".contact").click(function () {
    const userID = {
        userID: $(this).data("userid")
    };

    $(".chat-list li").removeClass("active");
    $(this).addClass("active");

    $.ajax({
        type: "get",
        url: "/Talk/Talk",
        data: userID,
        datatype: "json",
        success: function (html) {
            $("#talk").html(html);
        },
        error: function () {
            alert('error');
        }
    });
});