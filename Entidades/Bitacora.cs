using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Bitacora
    {
        
        public long Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public string Criticidad { get; set; }
        public string Detalle { get; set; }
        public long DVH { get; set; }

    }
}