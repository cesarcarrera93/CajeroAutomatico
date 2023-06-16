$(document).ready(function () {
    $('#balanceButton').click(function () {
        submitBalance();
    });

    $('#retiroButton').click(function () {
        submitRetiro();
    });

    $('#salirButton').click(function () {
        submitSalir();
    });

    verificarSession();
});

function submitBalance() {
    window.location.href = '/Operacion/Balance'
}

function submitRetiro() {
    window.location.href = '/Operacion/Retiro'
}

function submitSalir() {
    window.location.href = '/Acceso/Index'
}

function verificarSession() {
    if (sessionStorage.getItem('cardNumber') == null) {
        window.location.href = '/Acceso/Index'
    }
}
