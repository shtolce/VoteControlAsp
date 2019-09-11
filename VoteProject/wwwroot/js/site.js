let message;
let phone;
let FIO;
let messageText;
let phoneText;
let FIOText;

function fillDataClient() {
    phone = $('.modal-content__modal-body__inputPhone')[0];
    FIO = $('.modal-content__modal-body__inputFIO')[0];
    phoneText = phone.value;
    FIOText = FIO.value;
}

function fillDataMessage() {
    message = $('.modal-content__modal-body__inputMessage')[0];
    messageText = message.value;
}


function clearDataClient() {
    phone.value = "";
    FIO.value = "";
}
function clearDataMessage() {
    message.value = "";
}

function sendToAjax() {
    let data2 = {
        "message": messageText,
        "phone": phoneText,
        "FIO": FIOText 
    }

    $.post("/home/SendData", data2, function (data) {
        console.log(data);
    });



}


async function sendtoServerAsync() {
    await sendToAjax();
    //пост обработка дальше
}


function sendData() {
    sendtoServerAsync();
}


function clickEvent() {
    fillDataClient();
    alert(messageText + ' ' + phoneText + ' ' + FIOText);
    sendData();
    $('#modalClientData').modal('hide');
}

$('#sendMessage').on("click", clickEvent);

$('#modalClientData').on("hide.bs.modal", () => {
    fillDataClient();
    clearDataClient();

});

$('#modalMessageData').on("hide.bs.modal", () => {
    fillDataMessage();
    clearDataMessage();
});