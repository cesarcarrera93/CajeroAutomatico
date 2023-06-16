$(document).ready(function () {
    obtenerDatos();
});

function obtenerDatos() {
    var numeroTarjetaGuardado = sessionStorage.getItem('cardNumber');

    $.ajax({
        url: '/Operacion/TraerBalance',
        method: 'POST',
        data: { cardNumber: numeroTarjetaGuardado },
        success: function (response) {
            $('#numeroTarjeta').text(numeroTarjetaGuardado);
            $('#fechaVencimiento').text(moment(response.FechaVencimiento).format("DD/MM/YYYY"));
            $('#saldo').text(response.Saldo);
        },
        error: function (error) {
            console.error(error);
        }
    });
}
