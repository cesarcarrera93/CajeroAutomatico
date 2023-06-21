using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CajeroAutomatico.Models
{
    [Table("Tarjetas")]
    public class Tarjeta
    {
        [Key]
        public string NroTarjeta { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public decimal Saldo { get; set; }

        public string Pin { get; set; }

        public int Reintentos { get; set; }

        public bool Bloqueo { get; set; }

        //public virtual ICollection<Operacion> Operaciones { get; set; }

        public Tarjeta()
        {
            //Operaciones = new HashSet<Operacion>();
        }
    }
}
