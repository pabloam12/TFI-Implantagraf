using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Bitacora
    {
        [Display(Name = "Código")]
        public long Id { get; set; }

        [Display(Name = "Fecha y Hora")]
        public System.DateTime FechaHora { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [Display(Name = "Acción")]
        public string Accion { get; set; }

        [Display(Name = "Criticidad")]
        public string Criticidad { get; set; }

        public long DVH { get; set; }
    }
}