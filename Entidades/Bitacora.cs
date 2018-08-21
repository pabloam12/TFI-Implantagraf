using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Bitacora
    {
        public long ID { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaHora { get; set; }
        public long DVH { get; set; }
    }
}