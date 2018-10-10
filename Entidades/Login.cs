using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Login
    {
        [Required(ErrorMessage = "El {0} es Obligatorio")]
        public string Usuario { get; set; }


        [Required(ErrorMessage = "La {0} es Obligatoria")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

    }
}