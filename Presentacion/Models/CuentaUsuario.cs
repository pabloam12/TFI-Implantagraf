using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models
{
    public class CuentaUsuario
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Usr { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType (DataType.Password)]
        public string Psw { get; set; }
        [Required]
        public string ConfirmPsw { get; set; }

        [Required]
        public string CUIL { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int LocalidadId { get; set; }
        [Required]
        public System.DateTime FechaNacimiento { get; set; }
        [Required]
        public System.DateTime FechaAlta { get; set; }
        [Required]
        public int PerfilId { get; set; }
        [Required]
        public int IdiomaId { get; set; }
    }
}