using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CajeroAutomatico.Models
{
    public class Operacion
    {

        [Key]
        public int CodigoOperacion { get; set; }

        public int IdTipoOperacion { get; set; }

        public string NroTarjeta { get; set; }

        public DateTime FechaOperacion { get; set; }

        public decimal MontoRetirado { get; set; }

        //[ForeignKey("NroTarjeta")]
        //public virtual Tarjeta Tarjeta { get; set; }

        //[ForeignKey("IdTipoOperacion")]
        //public virtual TipoOperacion TipoOperacion { get; set; }
        public Operacion()
        {
        }

        public Operacion(int idTipoOperacion, string nroTarjeta, DateTime fechaOperacion, decimal montoRetirado)
        {
            IdTipoOperacion = idTipoOperacion;
            NroTarjeta = nroTarjeta;
            FechaOperacion = fechaOperacion;
            MontoRetirado = montoRetirado;
        }
    }
}