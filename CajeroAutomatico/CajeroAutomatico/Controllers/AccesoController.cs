using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CajeroAutomatico.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pin()
        {
            return View();
        }
        public ActionResult Bloqueo()
        {
            return View();
        }

        public ActionResult ValidarTarjeta(string cardNumber)
        {
            bool validacion = false;
            try
            {
                if (cardNumber == "1111111111111111") // hacer funcion validacion
                {
                    validacion = true;
                }

                var data = new
                {
                    validacion = validacion,
                    reintentos = 0
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) 
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }

        public ActionResult ValidarPin(string pin)
        {
            bool validacion = false;
            int reintentos = 0;
            try
            {
                if (pin == "1111")
                {
                    validacion = true;
                }
                else
                {
                    reintentos++; //= validacionReintentos();
                }

                var data = new
                {
                    validacion = validacion,
                    reintentos = reintentos
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }
    }
}