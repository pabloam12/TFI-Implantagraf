using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        //public string Nombre { get; set; }
        //public string Apellido { get; set; }
        //public string CUIL { get; set; }
        //public string Email { get; set; }
        public string Usr { get; set; }

        //[DataType(DataType.Password)]
        public string Psw { get; set; }
        //public string PswConfirmacion { get; set; }
        //public string Telefono { get; set; }
        //public string Direccion { get; set; }
        //public int LocalidadId { get; set; }

        ////[DataType(DataType.Date)]
        //public System.DateTime FechaNacimiento { get; set; }
        //public System.DateTime FechaAlta { get; set; }
        //public int PerfilId { get; set; }
        //public int IdiomaId { get; set; }
    }
}