using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int Tipo { get; set; }
        public double Monto { get; set; }
        public int FormaPagoId { get; set; }
        public string Direccion { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public int NroTarjeta { get; set; }
        public long DVH { get; set; }
    }
}