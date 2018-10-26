using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmLogin
    {

        [Required(ErrorMessage = "El Usuario es Obligatorio.")]
        [MinLength(5, ErrorMessage = "El {0} tiene que tener mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        public string Usuario { get; set; }


        [Required(ErrorMessage = "La Contraseña es Obligatoria")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

    }
}