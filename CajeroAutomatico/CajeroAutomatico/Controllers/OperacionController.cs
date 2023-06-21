using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CajeroAutomatico.Models;

namespace CajeroAutomatico.Controllers
{
    public class OperacionController : Controller
    {
        private CajeroAutomaticoDbContext db = new CajeroAutomaticoDbContext();
        // GET: Operacion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Balance()
        {
            return View();
        }
        public ActionResult Retiro()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult ReporteOperacion(string idReporteOperacion)
        {
            ViewBag.codigoOperacion = idReporteOperacion;
            return View();
        }
        public ActionResult TraerBalance(string cardNumber)
        {
            try
            {
                BalanceViewModel datos = ObtenerDatosBalance(cardNumber);
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }
        public ActionResult TraerReporteOperacion(string idReporteOperacion)
        {
            try
            {
                BalanceViewModel datos = ObtenerDatosReporteOperacion(idReporteOperacion);
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }
        public ActionResult RetirarMonto(int monto, string cardNumber)
        {
            try
            {
                int codigoOperacion = 0;
                bool validacion = validarSaldo(cardNumber, monto);

                if (validacion)
                {
                    Operacion operacion = retirarDinero(cardNumber, monto);
                    if (operacion != null)
                        codigoOperacion = operacion.CodigoOperacion;
                }

                var data = new
                {
                    saldoSuficiente = validacion,
                    codigoOperacion = codigoOperacion
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }

        private BalanceViewModel ObtenerDatosBalance(string cardNumber)
        {
            crearRegistroOperacion(cardNumber);
            Tarjeta tarjeta = ObtenerTarjeta(cardNumber);

            BalanceViewModel datos = new BalanceViewModel();
            datos.NumeroTarjeta = cardNumber;
            datos.FechaVencimiento = tarjeta.FechaVencimiento;
            datos.Saldo = tarjeta.Saldo;
            
            return datos;
        }

        private Tarjeta ObtenerTarjeta(string cardNumber)
        {
            Tarjeta tarjeta = db.Tarjetas.Find(cardNumber);
            return tarjeta;
        }

        private BalanceViewModel ObtenerDatosReporteOperacion(string idReporteOperacion)
        {
            BalanceViewModel datos = new BalanceViewModel();
            var operacion = db.Operaciones.Find(Convert.ToInt32(idReporteOperacion));
            if (operacion != null)
            {
                datos.NumeroTarjeta = operacion.NroTarjeta;
                datos.CantidadRetirada = operacion.MontoRetirado;
                datos.FechaReporte = operacion.FechaOperacion;
                datos.Saldo = ObtenerSaldoRestante(operacion.NroTarjeta);
            }

            return datos;
        }

        private Operacion crearRegistroOperacion(string cardNumber, int idTipoOperacion = 2, decimal retirarMonto = 0)
        {
            DateTime fechaOperacion = DateTime.Now;
            string fechaFormateada = fechaOperacion.ToString("dd/MM/yyyy HH:mm:ss");

            Operacion operacion = new Operacion();
            operacion.NroTarjeta = cardNumber;
            operacion.IdTipoOperacion = idTipoOperacion; // 1: retiro - 2: balance
                                                         //            operacion.FechaOperacion = DateTime.ParseExact(fechaFormateada, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            operacion.FechaOperacion = DateTime.Now;
            operacion.MontoRetirado = retirarMonto;

            db.Operaciones.Add(operacion);
            db.SaveChanges();

            return operacion;
        }

        private decimal ObtenerSaldoRestante(string cardNumber)
        {
            var tarjeta = db.Tarjetas.Find(cardNumber);
            if (tarjeta != null)
            {
                return tarjeta.Saldo;
            }

            return 0;
        }

        private bool validarSaldo(string cardNumber, int monto)
        {
            var tarjeta = db.Tarjetas.Find(cardNumber);
            if (tarjeta != null)
            {
                if((tarjeta.Saldo - monto) > 0)
                    return true;
            }

            return false;
        }

        private Operacion retirarDinero(string cardNumber, decimal monto)
        {
            Operacion operacion = new Operacion();
            Tarjeta tarjeta = db.Tarjetas.Find(cardNumber);

            if (tarjeta != null)
            {
                tarjeta.Saldo -= monto;
                db.SaveChanges();

                operacion = crearRegistroOperacion(cardNumber, 1, monto);
            }
            return operacion;
        }
    }
}