$(document).ready(function () {
    $('#balanceButton').click(function () {
        submitBalance();
    });

    $('#retiroButton').click(function () {
        submitRetiro();
    });

    verificarSession();
});

function submitBalance() {
    window.location.href = '/Operacion/Balance'
}

function submitRetiro() {
    window.location.href = '/Operacion/Retiro'
}

function verificarSession() {
    if (sessionStorage.getItem('cardNumber') == null) {
        window.location.href = '/Acceso/Index'
    }
}
