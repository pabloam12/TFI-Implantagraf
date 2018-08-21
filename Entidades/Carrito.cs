using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Carrito
    {
        public int Id { get; set; }
        public System.DateTime CarritoFecha { get; set; }
        public int CantidadItems { get; set; }
        public long DVH { get; set; }
    }
}