using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmOlvidoPsw
    {
        // Español
        [Required(ErrorMessage = "El Usuario es Obligatorio")]
        public string Usuario { get; set; }


        // Ingles
        [Required(ErrorMessage = "User is Obligatory")]
        public string Usuario_Eng { get; set; }

    }
}