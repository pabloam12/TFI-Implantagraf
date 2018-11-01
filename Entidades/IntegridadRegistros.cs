using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class IntegridadRegistros
    {
        [Display(Name = "CONFLICTO")]
        public string Col_A { get; set; }

        [Display(Name = "TABLA")]
        public string Col_B { get; set; }

        [Display(Name = "REGISTRO")]
        public string Col_C { get; set; }

        [Display(Name = "CAMPOS DE DETALLE")]
        public string Col_D { get; set; }

        [Display(Name = "DETALLE")]
        public string Col_E { get; set; }

        [Display(Name = "DETALLE")]
        public string Col_F { get; set; }

        [Display(Name = "DETALLE")]
        public string Col_G { get; set; }
                       
    }
}