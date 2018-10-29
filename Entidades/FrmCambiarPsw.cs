using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmCambiarPsw
    {

        [Required(ErrorMessage = "La Contraseña es Obligatoria")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,10}$", ErrorMessage = "La {0} debe tener entre 8 y 10 caracteres, al menos un número, al menos una minúscula y al menos una mayúscula.")]
        public string NuevaPsw { get; set; }

        [Required(ErrorMessage = "La Confirmación es Obligatoria")]
        [DataType(DataType.Password)]
        [Compare(nameof(NuevaPsw), ErrorMessage = "Las contraseñas no coinciden.")]
        public string PswConfirmacion { get; set; }

    }
}