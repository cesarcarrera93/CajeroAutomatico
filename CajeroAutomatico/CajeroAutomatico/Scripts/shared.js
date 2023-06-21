
function goBack() {
    window.location.href = '/Operacion/Index'
};

function salir() {
    removeSession();
    window.location.href = '/Acceso/Index';
}

function goIndex() {
    window.location.href = '/Operacion/Index';
}

function removeSession() {
    sessionStorage.removeItem('cardNumber');
}
