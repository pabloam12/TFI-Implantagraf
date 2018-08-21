using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usr { get; set; }
        public string Psw { get; set; }
        public string CUIL { get; set; }
        public string Activo { get; set; }
        public long Intentos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string LocalidadId { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public int PerfilId { get; set; }
        public int IdiomaId { get; set; }
        public long DVH { get; set; }
    }
}