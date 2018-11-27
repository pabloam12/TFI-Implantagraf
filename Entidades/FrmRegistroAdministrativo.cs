using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmRegistroAdministrativo
    {
        //Español
        [Required(ErrorMessage = "El Nombre es Obligatorio.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "El Nombre solo puede contener letras.")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El Apellido es Obligatorio.")]
        [MaxLength(50, ErrorMessage = "El {0} no puede superar los {1} caracteres.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "El Apellido solo puede contener letras.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El CUIL es Obligatorio")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "El {0} debe ser numérico de 11 dígitos.")]
        public string CUIL { get; set; }

        [Required(ErrorMessage = "El Email es Obligatorio")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "El {0} no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Usuario es Obligatorio.")]
        //[RegularExpression(@"/^[0 - 9a - zA - Z] +$/ ", ErrorMessage = "Solo se admiten letras y números sin espacios o símbolos.")]
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


        //Ingles
        [Required(ErrorMessage = "Name is Obligatory.")]
        [MaxLength(50, ErrorMessage = "Name can not have more than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Name can only have letters.")]
        public string Nombre_Eng { get; set; }


        [Required(ErrorMessage = "Surname is Obligatory.")]
        [MaxLength(50, ErrorMessage = "Surname can not have more than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Surname can only have letters.")]
        public string Apellido_Eng { get; set; }

        [Required(ErrorMessage = "CUIL is Obligatory.")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "CUIL must be 11 numbers long.")]
        public string CUIL_Eng { get; set; }

        [Required(ErrorMessage = "Email is Obligatory")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Invalid Email.")]
        public string Email_Eng { get; set; }

        [Required(ErrorMessage = "User is Obligatory.")]
        //[RegularExpression(@"/^[0 - 9a - zA - Z] +$/ ", ErrorMessage = "Solo se admiten letras y números sin espacios o símbolos.")]
        [MaxLength(50, ErrorMessage = "User can not be over 50 characters.")]
        [MinLength(5, ErrorMessage = "User can not be less than 5 characters.")]
        public string Usr_Eng { get; set; }

        [Required(ErrorMessage = "Psw is Obligatory.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,10}$", ErrorMessage = "The Psw must have between 8 and 10 characters, at least one number, at least one lowercase and at least one uppercase.")]
        public string Psw_Eng { get; set; }

        [Required(ErrorMessage = "Psw Confirmation is Obligatory.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Psw), ErrorMessage = "Psw do not match.")]
        public string PswConfirmacion_Eng { get; set; }

        [Required(ErrorMessage = "Address is Obligatory.")]
        [MaxLength(100, ErrorMessage = "Address can not be more than {1} characters.")]
        public string Direccion_Eng { get; set; }

        [Required(ErrorMessage = "Phone is Obligatory.")]
        [RegularExpression(@"[0-9]{1,25}(\.[0-9]{0,2})?$", ErrorMessage = "Phone can not have more than 25 dígits.")]
        [MinLength(8, ErrorMessage = "Phone must have no more than 8 digits.")]
        public string Telefono_Eng { get; set; }
    }
}