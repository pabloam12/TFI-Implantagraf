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
        public string RazonSocial { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CUIL { get; set; }
        public string Email { get; set; }
        public string Usr { get; set; }
        public string Psw { get; set; }
        public string PswConfirmacion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public Localidad Localidad { get; set; }
        public PerfilUsr PerfilUsr { get; set; }
        public Idioma Idioma { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public long DVH { get; set; }
    }
}