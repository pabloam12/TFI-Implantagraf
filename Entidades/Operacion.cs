using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Operacion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaHora { get; set; }
        public string TipoOperacion { get; set; }
        public double ImporteTotal { get; set; }
        public string Estado { get; set; }
        public int FormaPagoId { get; set; }
        public int FacturaId { get; set; }
        public long DVH { get; set; }

    }
}