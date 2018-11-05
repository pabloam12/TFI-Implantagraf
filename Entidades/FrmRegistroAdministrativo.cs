using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmRegistroAdministrativo
    {

        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El CUIL es Obligatorio")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "El {0} debe ser numérico de 11 dígitos.")]
        public string CUIL { get; set; }

        [Required(ErrorMessage = "El Email es Obligatorio")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "El {0} no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Usuario es Obligatorio.")]
        [RegularExpression(@"/^[0 - 9a - zA - Z] +$/ ", ErrorMessage = "Solo se admiten letras y números sin espacios o símbolos.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        [MinLength(5, ErrorMessage = "El {0} debe tener como mínimo {1} caracteres.")]
        public string Usr { get; set; }

        [Required(ErrorMessage = "La Contraseña es Obligatoria")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,10}$", ErrorMessage = "La {0} debe tener entre 8 y 10 caracteres, al menos un número, al menos una minúscula y al menos una mayúscula.")]
        public string Psw { get; set; }

        [Required(ErrorMessage = "La Confirmación es Obligatoria")]
        [DataType(DataType.Password)]
        [Compare(nameof(Psw), ErrorMessage = "Las contraseñas no coinciden.")]
        public string PswConfirmacion { get; set; }

        [Required(ErrorMessage = "La Dirección es Obligatoria")]
        [MaxLength(100, ErrorMessage = "La {0} no puede superar los {1} caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Telefono es Obligatorio")]
        [RegularExpression(@"[0-9]{1,25}(\.[0-9]{0,2})?$", ErrorMessage = "El {0} debe ser numérico y de no mas de 25 dígitos.")]
        [MinLength(8, ErrorMessage = "El Teléfono deber tener 8 dígitos como mínimo.")]
        public string Telefono { get; set; }

        

    }
}