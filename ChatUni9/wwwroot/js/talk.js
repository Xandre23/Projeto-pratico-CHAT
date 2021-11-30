"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();
$("#send").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    makeHTMLMessageReceive(getHour(), getDate(), message);
});

connection.start().then(function () {
    $("#send").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

$(document).on('click', '#send', function (event) {

    var user = $(this).data("iduserreceiver");
    var message = $("#txt-menssage").val();

    makeHTMLMessageSent(getHour(), getDate(), message);

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    $("#txt-menssage").val("");
    focusOnTheLastMessage();
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
    const spanMessageDateTime = factorySpanMessageDateTime(hour, formattedDate);

    const divMessageData = factoryDivMessageData("message-data text-right");
    divMessageData.append(spanMessageDateTime);

    const divMessage = factoryDivMessage("message other-message float-right", message);

    const li = factoryLI();
    li.append(divMessageData, divMessage);
    $("ul.chat-list-messages").append(li);
}

function factorySpanMessageDateTime(hour, formattedDate) {
    return jQuery("<span>", {
        "class": "message-data-time",
        "text": `${hour.format(new Date())} - ${formattedDate}`
    });
}

function makeHTMLMessageReceive(hour, formattedDate, message) {
    const spanMessageDateTime = factorySpanMessageDateTime(hour, formattedDate);

    const divMessageData = factoryDivMessageData("message-data");
    divMessageData.append(spanMessageDateTime);

    const divMessage = factoryDivMessage("message my-message", message);

    const li = factoryLI();
    li.append(divMessageData, divMessage);

    $("ul.chat-list-messages").append(li);
    focusOnTheLastMessage();
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
            $(".chat").html(html);
        },
        error: function () {
            alert('error');
        }
    });
});

function factoryLI() {
    return jQuery("<li>", {
        "class": "clearfix"
    });
}

function factoryDivMessage(classCss, message) {
    return jQuery("<div>", {
        "class": classCss,
        "text": message
    });
}

function factoryDivMessageData(classCss) {
    return jQuery("<div>", {
        "class": classCss
    });
}

function focusOnTheLastMessage() {
    $('.chat-list-messages').animate
        ({
            scrollTop: $('.chat-list-messages').prop("scrollHeight")
        }, 1000
    );
}