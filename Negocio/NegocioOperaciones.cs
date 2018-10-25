using AccesoDatos;
using Entidades;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class NegocioOperaciones
    {
        public Factura RegistrarFactura(DateTime fechaHora, string tipoFactura, decimal importeTotal, int formaPago, string estado, string direccion, string razonSocial, string email, string NroTarjeta = "N/A")
        {
            var datos = new OperacionesDAC();
            var inte = new IntegridadDatos();
            var aud = new Auditoria();

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
                Email = email,
                DVH = 0
            };

            var facturaActual = datos.RegistrarFactura(factura);

            facturaActual.DVH = inte.CalcularDVH(facturaActual.Codigo.ToString() + facturaActual.FechaHora.ToString() + facturaActual.Tipo + facturaActual.RazonSocial + facturaActual.Monto.ToString() + facturaActual.FormaPagoId.ToString() + facturaActual.NroTarjeta.ToString() + facturaActual.Direccion + facturaActual.Email + facturaActual.Estado);

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHFactura(facturaActual.Codigo, facturaActual.DVH);
            inte.RecalcularDVV("Factura");

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, "SISTEMA", "ALTA FACTURA", "INFO", "Se generó la factura: " + facturaActual.Codigo.ToString() + " para el Cliente " + facturaActual.RazonSocial + " por un Importe de $ " + facturaActual.Monto.ToString());

            return facturaActual;

        }

        public Operacion RegistrarVenta(DateTime fechaHora, int codCliente, decimal importeTotal, int formaPago, string tipoOperacion, string estado, int codFactura)
        {
            var datos = new OperacionesDAC();
            var datosUsuario = new CuentaDAC();
            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var venta = new Operacion
            {
                FechaHora = fechaHora,
                TipoOperacion = tipoOperacion,
                ImporteTotal = importeTotal,
                FormaPagoId = formaPago,
                Estado = estado,
                FacturaId = codFactura,
                DVH = 0
            };

            var operacionActual = datos.RegistrarOperacion(venta);

            var usuario = datosUsuario.ListarUsuarioPorId(codCliente);

            operacionActual.DVH = inte.CalcularDVH(operacionActual.Id.ToString() + operacionActual.ClienteId.ToString() + operacionActual.FechaHora.ToString() + operacionActual.TipoOperacion + operacionActual.ImporteTotal.ToString() + operacionActual.FacturaId.ToString());

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHOperacion(operacionActual.Id, operacionActual.DVH);
            inte.RecalcularDVV("Operacion");

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, usuario.Usr, "ALTA OPERACION VENTA", "INFO", "Se generó la Venta: " + operacionActual.Id.ToString() + " para el Cliente " + operacionActual.ClienteId + " por un Importe de $ " + operacionActual.ImporteTotal.ToString());
            

            return operacionActual;

        }


        public void RegistrarDetalleOperacion(int operacionId, int productoId, decimal monto, int cantidad, decimal subtotal, long DVH)
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