using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public int CUIL { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public Localidad Localidad { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public string Activo { get; set; }
        public long DVH { get; set; }
    }
}