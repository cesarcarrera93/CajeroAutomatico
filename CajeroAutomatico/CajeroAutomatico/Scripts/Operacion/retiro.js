$(document).ready(function () {
    $('.keypad-button').click(function () {
        var number = $(this).data('number');
        appendNumber(number);
    });

    $('#inputField').maskMoney({
        prefix: '$ ',
        thousands: ',',
        decimal: '.',
        allowZero: true, // Permitir valores de cero
        precision: 0 // Precisión de cero decimales
    });

    $('.clear-button').click(function () {
        clearInput();
    });

    $('.accept-button').click(function () {
        submitInput();
    });

    verificarSession();
    updateAcceptButton();
});

function verificarSession() {
    if (sessionStorage.getItem('cardNumber') == null) {
        window.location.href = '/Acceso/Index'
    }
}

function appendNumber(number) {
    var inputField = $('#inputField');
    var currentValue = inputField.val().replace(/[^0-9]+/g, ''); // Eliminar todos los caracteres no numéricos
    var newValue = currentValue + number;
    if (newValue.length <= 16) {
        newValue = formatCardNumber(newValue);
        inputField.val(newValue);
    }

    updateAcceptButton();
}

function formatCardNumber(cardNumber) {
    var formattedNumber = cardNumber.replace(/\D/g, ''); // Eliminar caracteres no numéricos
    var regex = /(\d{1,3})(?=(\d{3})+(?!\d))/g; // Expresión regular para separar en grupos de tres dígitos
    formattedNumber = formattedNumber.replace(regex, '$1.'); // Agregar puntos después de cada grupo de tres dígitos
    return '$ ' + formattedNumber;
}

function clearInput() {
    var inputField = $('#inputField');
    inputField.val('');

    updateAcceptButton();
}

function submitInput() {
    var inputField = $('#inputField'); 
    var monto = inputField.val().replace(/[^0-9]+/g, ''); // Eliminar todos los caracteres no numéricos
    var numeroTarjetaGuardado = sessionStorage.getItem('cardNumber');

    $.ajax({
        url: '/Operacion/RetirarMonto',
        method: 'POST',
        data: { monto: monto, cardNumber: numeroTarjetaGuardado },
        success: function (data) {
            console.log(data);

            if (data.validacion == "1")
                window.location.href = '/Operacion/ReporteOperacion'
            else if (data.saldoInsuficiente) {
                window.location.href = '/Operacion/Error'
            }
            else
                alert('PIN INCORRECTO. Reintentos restantes: ' + (4 - data.reintentos));
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function updateAcceptButton() {
    var inputField = $('#inputField');
    var monto = parseFloat(inputField.val().replace(/[^0-9.]+/g, '')); // Obtener el monto numérico
    var acceptButton = $('.accept-button');

    if (monto > 0) {
        acceptButton.removeClass('disabled');
    } else {
        acceptButton.addClass('disabled');
    }
}

