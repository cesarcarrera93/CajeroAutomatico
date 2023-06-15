$(document).ready(function () {
    $('.keypad-button').click(function () {
        var number = $(this).data('number');
        appendNumber(number);
    });

    $('.clear-button').click(function () {
        clearInput();
    });

    $('.accept-button').click(function () {
        submitInput();
    });

    $('#exitButton').click(function () {
        goBack();
    });

    updateAcceptButton();
});

function appendNumber(number) {
    var inputField = $('#inputField');
    var currentValue = inputField.val();
    var newValue = currentValue + number;
    if (newValue.length <= 4) {
        inputField.val(newValue);
    }

    updateAcceptButton();
}

function clearInput() {
    var inputField = $('#inputField');
    inputField.val('');

    updateAcceptButton();
}

function submitInput() {
    var pin = $('#inputField').val().replace(/-/g, '');

    $.ajax({
        url: '/Acceso/ValidarPin',
        method: 'GET',
        data: { pin: pin },
        success: function (data) {
            console.log(data);

            if (data.validacion == "1")
                window.location.href = '/Operacion/Index'
            else if (data.reintentos < 4)
                alert('PIN INCORRECTO. Reintentos restantes: ' + (4 - data.reintentos));
            else
                window.location.href = '/Acceso/Bloqueo'
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function goBack() {
    window.history.back();
}

function updateAcceptButton() {
    var inputField = $('#inputField');
    var cardNumber = inputField.val().replace(/-/g, '');
    var acceptButton = $('.accept-button');

    if (cardNumber.length === 4) {
        acceptButton.removeClass('disabled');
    } else {
        acceptButton.addClass('disabled');
    }
}