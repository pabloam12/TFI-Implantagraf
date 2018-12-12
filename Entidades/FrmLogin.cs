using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmLogin
    {
        //ESPAÑOL
        [Required(ErrorMessage = "El Usuario es Obligatorio.")]
        //[MinLength(5, ErrorMessage = "El {0} tiene que tener mínimo {1} caracteres.")]
        //[MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        public string Usuario { get; set; }


        [Required(ErrorMessage = "La Contraseña es Obligatoria")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        
        //INGLES
        [Required(ErrorMessage = "User is Required.")]
        //[MinLength(5, ErrorMessage = "The User must have a minimum of {1} characters.")]
        //[MaxLength(50, ErrorMessage = "The User must have a maximun of {1} characters.")]
        public string Usuario_Eng { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Contraseña_Eng { get; set; }

    }
}