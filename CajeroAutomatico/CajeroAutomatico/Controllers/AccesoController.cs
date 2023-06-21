using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CajeroAutomatico.Models;

namespace CajeroAutomatico.Controllers
{
    public class AccesoController : Controller
    {
        private CajeroAutomaticoDbContext db = new CajeroAutomaticoDbContext();
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
                if (tarjetaEsValida(cardNumber))
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

        public ActionResult ValidarPin(string pin, string cardNumber)
        {
            bool validacion = false;
            int reintentos = 0;
            bool bloqueo = false;
            try
            {
                if (pinEsValido(pin, cardNumber))
                {
                    validacion = true;
                    reiniciarReintentos(cardNumber);// se reinicia el contador de reintentos
                }
                else
                {
                    reintentos= validarReintentos(pin, cardNumber); // se suma un reintento fallido y valida la cantidad de reintentos
                    if (reintentos >= 4)
                    {
                        bloquearTarjeta(cardNumber);
                        bloqueo = true;
                    }
                }

                var data = new
                {
                    validacion = validacion,
                    reintentos = reintentos,
                    bloqueo = bloqueo
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content("ocurrio un error: " + ex.Message);
            }
        }

        private bool tarjetaEsValida(string cardNumber)
        {
            try
            {
                Tarjeta tarjeta = new Tarjeta();
                tarjeta = db.Tarjetas.FirstOrDefault(c => c.NroTarjeta == cardNumber);
                if (tarjeta != null)
                {
                    if (!tarjeta.Bloqueo)
                        return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool pinEsValido(string pin, string cardNumber)
        {
            
            Tarjeta tarjeta = new Tarjeta();
            
            tarjeta = db.Tarjetas.FirstOrDefault(c => c.NroTarjeta == cardNumber);
            
            if (tarjeta != null)
            {
                if (pin == tarjeta.Pin)
                {
                    return true;
                }
            }
                
            return false;  
        }

        private int validarReintentos(string pin, string cardNumber)
        {
            var tarjeta = db.Tarjetas.Find(cardNumber);
            if (tarjeta != null)
            {
                tarjeta.Reintentos += 1;
                db.SaveChanges();
                return tarjeta.Reintentos;
            }
            return -1;
        }

        private void bloquearTarjeta(string cardNumber)
        {
            var tarjeta = db.Tarjetas.Find(cardNumber);
            if (tarjeta != null)
            {
                tarjeta.Bloqueo = true;
                db.SaveChanges();
            }
            reiniciarReintentos(cardNumber);
        }

        private void reiniciarReintentos(string cardNumber)
        {
            var tarjeta = db.Tarjetas.Find(cardNumber);
            if (tarjeta != null)
            {
                tarjeta.Reintentos = 0;
                db.SaveChanges();
            }
        }
        
    }
}