using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class DetalleOperacion
    {
        public int OperacionId { get; set; }
        public int ProductoId { get; set; }
        public double Monto { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }

        public long DVH { get; set; }
    }
}