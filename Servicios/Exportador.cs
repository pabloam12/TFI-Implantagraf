using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Servicios
{
    public class Exportador
    {

        public void ExportarBitacoraXML(List<Bitacora> listaBitacora)
        {

            var fechaHora = DateTime.Now.ToShortDateString();
            var dia = fechaHora.Substring(0, 2);
            var mes = fechaHora.Substring(3, 2);
            var anio = fechaHora.Substring(6, 4);

            using (XmlWriter writer = XmlWriter.Create("C:\\Implantagraf\\XML\\Bitacora_" + anio + mes + dia + ".xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("BitacoraXML");

                foreach (Bitacora bitacoraActual in listaBitacora)
                {
                    writer.WriteStartElement("Bitacora");

                    writer.WriteAttributeString("Id", bitacoraActual.Id.ToString());

                    writer.WriteElementString("FechaHora", bitacoraActual.FechaHora.ToString());
                    writer.WriteElementString("Usuario", bitacoraActual.Usuario);
                    writer.WriteElementString("Accion", bitacoraActual.Accion);
                    writer.WriteElementString("Criticidad", bitacoraActual.Criticidad);
                    writer.WriteElementString("Detalle", bitacoraActual.Detalle);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }


        public void ExportarClientesXML(List<Usuario> listaClientes)
        {
            var fechaHora = DateTime.Now.ToShortDateString();
            var dia = fechaHora.Substring(0, 2);
            var mes = fechaHora.Substring(3, 2);
            var anio = fechaHora.Substring(6, 4);

            using (XmlWriter writer = XmlWriter.Create("C:\\Implantagraf\\XML\\Clientes_" + anio + mes + dia + ".xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("ClientesXML");

                foreach (Usuario clienteActual in listaClientes)
                {
                    writer.WriteStartElement("Cliente");

                    writer.WriteAttributeString("Id", clienteActual.Id.ToString());

                    writer.WriteElementString("FechaAlta", clienteActual.FechaAlta.ToString());
                    writer.WriteElementString("RazonSocial", clienteActual.RazonSocial);
                    writer.WriteElementString("CUIL", clienteActual.CUIL);
                    writer.WriteElementString("Email", clienteActual.Email);
                    writer.WriteElementString("Telefono", clienteActual.Telefono);
                    writer.WriteElementString("Direccion", clienteActual.Direccion);
                    writer.WriteElementString("Descripcion", clienteActual.Localidad.Descripcion);
                    
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }


        public void ExportarVentasXML(List<Operacion> listaVentas)
        {
            var fechaHora = DateTime.Now.ToShortDateString();
            var dia = fechaHora.Substring(0, 2);
            var mes = fechaHora.Substring(3, 2);
            var anio = fechaHora.Substring(6, 4);

            using (XmlWriter writer = XmlWriter.Create("C:\\Implantagraf\\XML\\Ventas_" + anio + mes + dia + ".xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("VentasXML");

                foreach (Operacion ventaActual in listaVentas)
                {
                    writer.WriteStartElement("Venta");

                    writer.WriteAttributeString("Codigo", ventaActual.Id.ToString());

                    writer.WriteElementString("FechaHora", ventaActual.FechaHora.ToString());
                    writer.WriteElementString("Cliente", ventaActual.Cliente.RazonSocial);                    
                    writer.WriteElementString("Importe", ventaActual.ImporteTotal.ToString());
                    writer.WriteElementString("Estado", ventaActual.Estado.Descripcion);
                    writer.WriteElementString("FormaPago", ventaActual.FormaPago.Descripcion);
                    writer.WriteElementString("Factura", ventaActual.Factura.Codigo.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }



    }
}