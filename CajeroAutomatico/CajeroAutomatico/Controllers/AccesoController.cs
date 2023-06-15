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
            try
            {
                return Content("1");
            }
            catch (Exception ex) 
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }

        public ActionResult ValidarPin(string pin)
        {
            try
            {
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }
    }
}