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
        public string CUIL { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public Localidad Localidad { get; set; }
        public DateTime FechaAlta { get; set; }
        public long DVH { get; set; }
    }
}