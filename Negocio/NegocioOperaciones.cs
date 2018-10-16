using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class NegocioOperaciones
    {
        public Factura RegistrarFactura(DateTime fechaHora, string tipoFactura, double importeTotal, int formaPago, string direccion, string razonSocial, string email)
        {
            var datos = new OperacionesDAC();

            var factura = new Factura
            {
                FechaHora = fechaHora,
                Tipo = tipoFactura,
                Monto = importeTotal,
                FormaPagoId=formaPago,
                Direccion= direccion,
                RazonSocial=razonSocial,
                Email=email
            };


            return datos.RegistrarFactura(factura, 54564546);

        }

        //public int RegistrarVenta(int codCliente, double importeTotal, string estado, int codFactura)
        //{
        //    datos = new OperacionesDAC();

        //    return datos.RegistrarVenta(codCliente, "VENTA", importeTotal, estado, codFactura, 456465);

        //} 

    }
}