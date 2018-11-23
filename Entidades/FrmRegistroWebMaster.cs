using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmRegistroWebMaster
    {
        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "El Nombre solo puede contener letras.")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "El Apellido solo puede contener letras.")]
        public string Apellido { get; set; }
        

        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "El {0} no es válido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "El Usuario es Obligatorio.")]
        //[RegularExpression(@"/^[0 - 9a - zA - Z] +$/ ", ErrorMessage = "Solo se admiten letras y números sin espacios o símbolos.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        [MinLength(5, ErrorMessage = "El {0} debe tener como mínimo {1} caracteres.")]
        public string Usr { get; set; }


        [Required(ErrorMessage = "La Contraseña es Obligatoria.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,10}$", ErrorMessage = "La {0} debe tener entre 8 y 10 caracteres, al menos un número, al menos una minúscula y al menos una mayúscula.")]
        public string Psw { get; set; }


        [Required(ErrorMessage = "La Confirmación es Obligatoria.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Psw), ErrorMessage = "Las contraseñas no coinciden.")]
        public string PswConfirmacion { get; set; }
         

    }
}