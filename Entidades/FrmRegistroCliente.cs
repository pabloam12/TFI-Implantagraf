using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmRegistroCliente
    {
        //Español
        [Required(ErrorMessage = "La Razón Social es Obligatoria")]
        [MaxLength(50, ErrorMessage = "La Razón Social no puede superar los {1} caracteres.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "La Razón Social solo puede contener letras.")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El CUIL es Obligatorio")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "El {0} debe ser numérico de 11 dígitos.")]
        public string CUIL { get; set; }

        [Required(ErrorMessage = "El Email es Obligatorio")]
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
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "La Dirección solo puede contener letras y números.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Telefono es Obligatorio")]
        [RegularExpression(@"[0-9]{1,25}(\.[0-9]{0,2})?$", ErrorMessage = "El {0} debe ser numérico y de no mas de 25 dígitos.")]
        [MinLength(8, ErrorMessage = "El Teléfono deber tener 8 dígitos como mínimo.")]
        public string Telefono { get; set; }


        //Ingles
        [Required(ErrorMessage = "The Company Name is Required")]
        [MaxLength(50, ErrorMessage = "The Company Name must have a maximun of {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Company Name must have only letters.")]
        public string RazonSocial_Eng { get; set; }

        [Required(ErrorMessage = "CUIL is Required")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "CUIL must be numeric with 11 digits.")]
        public string CUIL_Eng { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Invalid Email.")]
        public string Email_Eng { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,10}$", ErrorMessage = "The password must have between 8 and 10 characters, at least one number, at least one lowercase and at least one uppercase.")]
        public string Psw_Eng { get; set; }

        [Required(ErrorMessage = "Password confirm is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Psw_Eng), ErrorMessage = "Passwords do not match.")]
        public string PswConfirmacion_Eng { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [MaxLength(100, ErrorMessage = "Address must have a maximun of {1} characters.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Address must have only letters and numbers.")]
        public string Direccion_Eng { get; set; }

        [Required(ErrorMessage = "Phone number is Required")]
        [RegularExpression(@"[0-9]{1,25}(\.[0-9]{0,2})?$", ErrorMessage = "Phone number must be numeric and have a maximun of 25 characters.")]
        [MinLength(8, ErrorMessage = "Phone number must have a maximun of 8 characters.")]
        public string Telefono_Eng { get; set; }

        public Localidad Localidad { get; set; }

    }
}