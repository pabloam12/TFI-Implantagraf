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
        public Factura RegistrarFactura(DateTime fechaHora, string tipoFactura, int importeTotal, int formaPagoId, int estadoId, int clienteId, string NroTarjeta = "N/A")
        {
            var datos = new OperacionesDAC();
            var inte = new IntegridadDatos();
            var aud = new Auditoria();
            var clienteDatos = new ClienteDAC();
            var estadoOperacionDatos = new EstadoOperacionDAC();
            var accDatosFormaPago = new FormaPagoDAC();

            var factura = new Factura
            {
                FechaHora = fechaHora,
                Tipo = tipoFactura,
                Monto = importeTotal,
                FormaPago = accDatosFormaPago.BuscarPorId(formaPagoId), //Mapper FormaPago.
                Estado = estadoOperacionDatos.BuscarPorId(estadoId), //Mapper EstadoOperacion.
                Cliente = clienteDatos.BuscarPorId(clienteId), // Mapper Cliente.

            };

            var facturaActual = datos.RegistrarFactura(factura);

            var facturaDVH = inte.CalcularDVH(facturaActual.Codigo.ToString() + facturaActual.FechaHora.ToString() + facturaActual.Tipo + facturaActual.Cliente.Id.ToString() + facturaActual.Monto.ToString() + facturaActual.FormaPago.Id.ToString() + facturaActual.Estado.Id.ToString());

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHFactura(facturaActual.Codigo, facturaDVH);
            inte.RecalcularDVV("Factura");

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, "SISTEMA", "ALTA FACTURA", "INFO", "Se generó la factura: " + facturaActual.Codigo.ToString() + " para el Cliente " + facturaActual.Cliente.Id + " por un Importe de $ " + facturaActual.Monto.ToString() + " con estado " + facturaActual.Estado.Descripcion);

            return facturaActual;

        }

        public Operacion RegistrarOperacion(DateTime fechaHora, int codCliente, int importeTotal, int formaPagoId, string tipoOperacion, int estadoId, int codFactura)
        {
            var datos = new OperacionesDAC();
            var datosUsuario = new CuentaDAC();
            var inte = new IntegridadDatos();
            var aud = new Auditoria();
            var accDatosEstadoOperacion = new EstadoOperacionDAC();
            var accDatosCliente = new ClienteDAC();
            var accDatosFormaPago = new FormaPagoDAC();


            var operacion = new Operacion
            {
                FechaHora = fechaHora,
                Cliente = accDatosCliente.BuscarPorId(codCliente), //Mapper Cliente.
                TipoOperacion = tipoOperacion, //TODO reemplazar String por Clase nueva si hay tiempo. 
                ImporteTotal = importeTotal,
                FormaPago = accDatosFormaPago.BuscarPorId(formaPagoId), //Mapper FormaPago.
                Estado = accDatosEstadoOperacion.BuscarPorId(estadoId), //Mapper EstadoOperacion.
                Factura = datos.BuscarFacturaporCodigo(codFactura) //Mapper Factura.

            };

            var operacionActual = datos.RegistrarOperacion(operacion);

            operacionActual.DVH = inte.CalcularDVH(operacionActual.Id.ToString() + operacionActual.Cliente.Id.ToString() + operacionActual.FechaHora.ToString() + operacionActual.TipoOperacion + operacionActual.ImporteTotal.ToString() + operacionActual.Factura.Codigo.ToString() + operacionActual.Estado.Id.ToString());

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHOperacion(operacionActual.Id, operacionActual.DVH);
            inte.RecalcularDVV("Operacion");

            var usuario = datosUsuario.ListarUsuarioPorId(codCliente);

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, usuario.Usr, "ALTA OPERACION VENTA", "INFO", "Se generó la Venta: " + operacionActual.Id.ToString() + " para el Cliente " + operacionActual.Cliente.Id + " por un Importe de $ " + operacionActual.ImporteTotal.ToString());


            return operacionActual;

        }


        public void RegistrarDetalleOperacion(DetalleOperacion detalleActual)
        {
            var datos = new OperacionesDAC();
            var inte = new IntegridadDatos();

            datos.RegistrarDetalleOperacion(detalleActual);

        }

        public List<DetalleOperacion> ListarDetalleOperacion()
        {
            var datos = new OperacionesDAC();

            return datos.ListarDetalleOperacion();

        }

        public List<Operacion> ListarVentasPorCliente(string idCliente)
        {
            var datos = new OperacionesDAC();

            return datos.ListarVentasPorCliente(idCliente);

        }
        

        public List<Operacion> ListarOperacionesPorTipo(string tipo)
        {
            var datos = new OperacionesDAC();

            return datos.ListarOperacionesporTipo(tipo);

        }

        public List<Operacion> ListarVentasPorFiltro(string fecha, string fechaFin)
        {
            var datos = new OperacionesDAC();

            return datos.ListarVentasPorFiltro(fecha, fechaFin);

        }



        public List<DetalleOperacion> ListarDetalleporOperacion(int id)
        {
            var datos = new OperacionesDAC();

            return datos.ListarDetalleOperacion();

        }

    }
}