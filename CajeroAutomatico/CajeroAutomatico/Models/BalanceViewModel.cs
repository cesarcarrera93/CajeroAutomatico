﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CajeroAutomatico.Models
{
    public class BalanceViewModel
    {
        public string NumeroTarjeta { get; set; }
        public decimal Saldo { get; set; }
        public int? idReporte { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? CantidadRetirada { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string codigoOperacion { get; set; }

        public BalanceViewModel()
        {
        }

        public BalanceViewModel(string numeroTarjeta, DateTime fechaVencimiento, decimal saldo)
        {
            NumeroTarjeta = numeroTarjeta;
            FechaVencimiento = fechaVencimiento;
            Saldo = saldo;
        }
    }
}