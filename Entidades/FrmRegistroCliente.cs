using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmRegistroCliente
    {
        
        [Required(ErrorMessage = "La Razón Social es Obligatoria")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "El {0} debe ser numérico de 11 dígitos.")]
        public string CUIL { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "El {0} no es válido.")]
        public string Email { get; set; }
        
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

        [Required(ErrorMessage = "La Localidad es Obligatoria.")]
        public Localidad Localidad { get; set; }
                
    }
}