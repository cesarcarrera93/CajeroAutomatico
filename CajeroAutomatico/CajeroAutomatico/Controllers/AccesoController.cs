﻿using System;
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
                }
                else
                {
                    reintentos= validacionReintentos(pin, cardNumber);
                    if (reintentos >= 4)
                    {
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
            if (cardNumber == "1111111111111111")
                return true;

            return false;
        }

        private bool pinEsValido(string pin, string cardNumber)
        {
            if (pin == "1111")
                return true;

            return false;
        }

        private int validacionReintentos(string pin, string cardNumber)
        {
            return 1;
        }
    }
}