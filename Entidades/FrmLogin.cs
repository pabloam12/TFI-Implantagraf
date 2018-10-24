using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmLogin
    {

        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        [MinLength(5, ErrorMessage = "El {0} tiene que tener mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        public string Usuario { get; set; }


        [Required(ErrorMessage = "La {0} es Obligatoria")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,10}$", ErrorMessage = "La {0} debe tener entre 8 y 10 caracteres, al menos un número, al menos una minúscula y al menos una mayúscula.")]
        public string Contraseña { get; set; }

    }
}