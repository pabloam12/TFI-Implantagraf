using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Usuario
    {
        [Key]
        [Display(Name ="Id de Usuario")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [RegularExpression(@"[a-zA-ZñÑ\s]{2,50}", ErrorMessage = "El {0} debe ser válido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [RegularExpression(@"[a-zA-ZñÑ\s]{2,50}", ErrorMessage = "El {0} debe ser válido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        //[RegularExpression(@"[0-9]{1,11}?$", ErrorMessage ="El {0} debe ser numérico y no mayor a 11 caracteres.")]
        public string CUIL { get; set; }

        [Required(ErrorMessage = "El {0} es Obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El {0} debe ser un mail válido")]
        public string Email { get; set; }

        public string Usr { get; set; }
        
        [Required(ErrorMessage = "La {0} es Obligatoria")]
        [DataType(DataType.Password)]
        public string Psw { get; set; }

        [Required(ErrorMessage = "La {0} es Obligatoria")]
        [DataType(DataType.Password)]
        [Compare(nameof(Psw), ErrorMessage = "Las contraseñas no coinciden.")]
        public string PswConfirmacion { get; set; }

        public string Telefono { get; set; }

        
        public string Direccion { get; set; }

        public Localidad Localidad { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime FechaNacimiento { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime FechaAlta { get; set; }

        public PerfilUsr Perfil { get; set; }
        public Idioma Idioma { get; set; }
    }
}