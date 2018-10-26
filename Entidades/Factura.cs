using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Factura
    {
        public int Codigo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Tipo { get; set; }
        public int Monto { get; set; }
        public FormaPago FormaPago { get; set; }
        public EstadoOperacion Estado { get; set; }
        public Cliente Cliente { get; set; }
        public long DVH { get; set; }
    }
}