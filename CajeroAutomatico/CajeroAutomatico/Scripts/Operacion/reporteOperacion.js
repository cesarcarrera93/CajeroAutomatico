$(document).ready(function () {
    obtenerDatos();
});

function obtenerDatos() {
    var numeroTarjetaGuardado = sessionStorage.getItem('cardNumber');

    $.ajax({
        url: '/Operacion/TraerReporteOperacion',
        method: 'GET',
        data: { IdReporteOperacion: idReporteOperacion },
        success: function (response) {
            $('#numeroTarjeta').text(numeroTarjetaGuardado);
            $('#fechareporte').text(moment(response.FechaReporte).format("DD/MM/YYYY HH:mm"));
            $('#retiro').text(response.CantidadRetirada);
            $('#saldo').text(response.Saldo);
        },
        error: function (error) {
            console.error(error);
        }
    });
}