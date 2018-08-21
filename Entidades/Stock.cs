using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public System.DateTime FechaCalendario { get; set; }
        public int Stock1 { get; set; }
        public int TipoOperacionId { get; set; }
    }
}