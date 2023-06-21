using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CajeroAutomatico.Models
{
    [Table("TipoOperacion")]
    public class TipoOperacion
    {
        [Key]
        public int IdTipoOperacion { get; set; }

        public string Operacion { get; set; }

        //public virtual ICollection<Operacion> Operaciones { get; set; }

        public TipoOperacion()
        {
            //Operaciones = new HashSet<Operacion>();
        }

        public TipoOperacion(string operacion)
        {
            Operacion = operacion;
            //Operaciones = new HashSet<Operacion>();
        }
    }
}