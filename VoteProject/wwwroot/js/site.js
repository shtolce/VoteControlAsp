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

function sendDataOk(txt) {
    let data2 = {
        "message": txt,
        "phone": "No contact details provided.",
        "FIO": ""
    }

    jQuery.ajax({
        url: "/home/SendDataOk",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data2),
        success: function (data) {
        }
    });


}



function sendToAjax(sourceBut) {
    if (sourceBut) messageText = sourceBut; 
    let data2 = {
        "message": messageText,
        "phone": phoneText,
        "FIO": FIOText 
    }

    jQuery.ajax({
        url: "/home/SendData",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data2),
        success: function (data) {
            //console.log(data);
        }
    });


}


async function sendtoServerAsync(sourceBut) {
    await sendToAjax(sourceBut);
    //пост обработка дальше
}


function sendData(sourceBut) {
    sendtoServerAsync(sourceBut);
}


function clickEvent(e) {
    fillDataClient();
    let sourceBut = $(e.target).data('whatever');
    $(e.target).removeAttr("data-whatever");
    $('#modalClientData #sendMessage').removeData(["whatever"])
    sendData(sourceBut);
    $('#modalClientData').modal('hide');
}

function clickEventOk() {
    sendDataOk("Ok");
}
function clickEventNeutral() {
    sendDataOk("Neutral");
}

$('#sendMessage').on("click", clickEvent);
$('#btnNeutral').on("click", clickEventNeutral);
$('#btnOk').on("click", clickEventOk);

$('#modalClientData').on("show.bs.modal", (e) => {
    let sourceBut = $(e.relatedTarget).data('whatever');
    $('#modalClientData #sendMessage').attr("data-whatever", sourceBut);
});

$('#modalClientData').on("hide.bs.modal", () => {
    fillDataClient();
    clearDataClient();

});

$('#modalMessageData').on("hide.bs.modal", () => {
    fillDataMessage();
    clearDataMessage();
});

$('#btnNeutral').popover({
    placement: 'down',
    trigger: 'manual',
    placement: 'bottom',
    html: 'true',
    template: '<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header">Ваша оценка принята\
                </h3> <div class="popover-body"><h4 style="font-family:verdana;color:red;" >Спасибо за ваш отзыв</h4></div></div> '

});
$('#btnOk').popover({
    placement: 'down',
    trigger: 'manual',
    placement: 'bottom',
    template: '<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-header">Ваша оценка принята\
                </h3> <h4><div class="popover-body">Спасибо за ваш отзыв</div></h4></div> '

});

$('#btnNeutral').on('click', (e) => {
    $('#btnNeutral').popover('show');

    setTimeout(() => {
        $(e.target).popover('hide');
    }, 3000);
    
});
$('#btnOk').on('click', (e) => {
    $('#btnOk').popover('show');
    setTimeout(() => {
        $(e.target).popover('hide');
    }, 3000);

});


function set_cookie(name, value, exp_y, exp_m, exp_d, path, domain, secure) {
    var cookie_string = name + "=" + escape(value);
    if (exp_y) {
        var expires = new Date(exp_y, exp_m, exp_d);
        cookie_string += "; expires=" + expires.toGMTString();
    }
    if (path)
        cookie_string += "; path=" + escape(path);
    if (domain)
        cookie_string += "; domain=" + escape(domain);
    if (secure)
        cookie_string += "; secure";
    document.cookie = cookie_string;
}


$('#SaveSettings').on('click', (e) => {
    if (md5.hex($('#passwordModalAdmin').val()) == '5583413443164b56500def9a533c7c70') {
        set_cookie("fillialName", $('#fillialModalAdmin').val());
        alert('Данные сохранены успешно');
    }
    else {
        alert('Пароль введен неверно данные не будут сохранены')
    }



});

