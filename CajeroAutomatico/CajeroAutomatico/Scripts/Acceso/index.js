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

    updateAcceptButton();
});

function appendNumber(number) {
    var inputField = $('#inputField');
    var currentValue = inputField.val();
    var newValue = currentValue + number;
    if (newValue.length <= 19) {
        newValue = formatCardNumber(newValue);
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
    var cardNumber = $('#inputField').val().replace(/-/g, '');

    $.ajax({
        url: '/Acceso/ValidarTarjeta',
        method: 'GET',
        data: { cardNumber: cardNumber },
        success: function (data) {
            console.log(data);

            if (data.validacion == "1")
                window.location.href = '/Acceso/Pin'
            else
                alert('La tarjeta esta bloqueada o no existe');
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function formatCardNumber(cardNumber) {
    var formattedNumber = cardNumber.replace(/\D/g, ''); // Eliminar caracteres no numéricos
    var regex = /(\d{4})(?=\d)/g; // Expresión regular para separar en grupos de 4 dígitos
    formattedNumber = formattedNumber.replace(regex, '$1-');
    return formattedNumber;
}

function updateAcceptButton() {
    var inputField = $('#inputField');
    var cardNumber = inputField.val().replace(/-/g, '');
    var acceptButton = $('.accept-button');

    if (cardNumber.length === 16) {
        acceptButton.removeClass('disabled');
    } else {
        acceptButton.addClass('disabled');
    }
}