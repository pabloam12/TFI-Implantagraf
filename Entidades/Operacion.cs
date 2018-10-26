using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Operacion
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaHora { get; set; }
        public string TipoOperacion { get; set; }
        public int ImporteTotal { get; set; }
        public EstadoOperacion Estado { get; set; }
        public FormaPago FormaPago { get; set; }
        public Factura Factura { get; set; }
        public List<DetalleOperacion> DetalleProductos { get; set; }
        public long DVH { get; set; }

    }
}