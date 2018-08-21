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
        public System.DateTime FechaOperacion { get; set; }
        public int TipoOperacionId { get; set; }
        public double MontoTotal { get; set; }
        public Nullable<int> Estado { get; set; }
        public int FacturaId { get; set; }
        public long DVH { get; set; }

    }
}