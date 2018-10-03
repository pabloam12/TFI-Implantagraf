using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Respaldo
    {
        [Display(Name = "Copia de Respaldo")]
        public string Ruta { get; set; }
        
    }
}