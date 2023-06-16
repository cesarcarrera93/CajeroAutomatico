using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CajeroAutomatico.Models;

namespace CajeroAutomatico.Controllers
{
    public class OperacionController : Controller
    {
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

        private BalanceViewModel ObtenerDatosBalance(string cardNumber)
        {
            // crear un registro de operación en la tabla con el ID de la tarjeta, 
            // el momento en el tiempo y el código de la operación.
            
            DateTime fechaVencimiento = new DateTime(2023, 12, 31);
            BalanceViewModel datos = new BalanceViewModel("1111111111111111", fechaVencimiento, 2000);
            return datos;
        }
    }
}