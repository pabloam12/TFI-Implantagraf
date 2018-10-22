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
        public Factura RegistrarFactura(DateTime fechaHora, string tipoFactura, double importeTotal, int formaPago, string estado, string direccion, string razonSocial, string email, double NroTarjeta = 0)
        {
            var datos = new OperacionesDAC();

            var factura = new Factura
            {
                FechaHora = fechaHora,
                Tipo = tipoFactura,
                Monto = importeTotal,
                FormaPagoId = formaPago,
                Estado = estado,
                Direccion = direccion,
                RazonSocial = razonSocial,
                NroTarjeta = NroTarjeta,
                Email = email
            };

            factura.DVH = 231232;

            return datos.RegistrarFactura(factura);

        }

        public Operacion RegistrarVenta(DateTime fechaHora, int codCliente, double importeTotal, int formaPago, string tipoOperacion, string estado, int codFactura)
        {
            var datos = new OperacionesDAC();

            var venta = new Operacion
            {
                FechaHora = fechaHora,
                TipoOperacion = tipoOperacion,
                ImporteTotal = importeTotal,
                FormaPagoId = formaPago,
                Estado = estado,
                FacturaId = codFactura
            };

            venta.DVH = 23231;


            return datos.RegistrarOperacion(venta);

        }


        public void RegistrarDetalleOperacion(int operacionId, int productoId, double monto, int cantidad, double subtotal, long DVH)
        {

            var datos = new OperacionesDAC();

            datos.RegistrarDetalleOperacion(operacionId, productoId, monto, cantidad, subtotal, DVH);
        }

        public List<DetalleOperacion> ListarDetalleOperacion()
        {
            var datos = new OperacionesDAC();

            return datos.ListarDetalleOperacion();

        }

        public List<DetalleOperacion> ListarDetalleporOperacion(int id)
        {
            var datos = new OperacionesDAC();

            return datos.ListarDetalleOperacion();

        }

    }
}