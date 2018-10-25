using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Factura
    {
        public int Codigo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public int FormaPagoId { get; set; }
        public string Estado { get; set; }
        public string Direccion { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string NroTarjeta { get; set; }
        public long DVH { get; set; }
    }
}