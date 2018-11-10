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

        //public String GenerarFacturaPDF(Factura oFactura, List<Vehiculo> lstVehiculos)
        //{

        //    string pdfTemplate = @Server.MapPath("~/Carrito/factura_" + oFactura.IdFactura + ".pdf");

        //    PdfReader pdfReader = null;

        //    // Create the form filler
        //    FileStream pdfOutputFile = new FileStream(pdfTemplate, FileMode.Create);

        //    pdfReader = new PdfReader(@Server.MapPath("~/Carrito/e-factura.pdf"));

        //    PdfStamper pdfStamper = null;

        //    pdfStamper = new PdfStamper(pdfReader, pdfOutputFile);

        //    // Get the form fields
        //    AcroFields testForm = pdfStamper.AcroFields;

        //    // Datos de la factura
        //    testForm.SetField("factura", oFactura.IdFactura.ToString());
        //    testForm.SetField("tipo_factura", " B");
        //    testForm.SetField("pagina_de", "1");
        //    testForm.SetField("pagina_hta", "1");
        //    testForm.SetField("nombre_cliente", oFactura.NombreCliente);
        //    testForm.SetField("direccion_cliente", "Direccion: " + oFactura.DireccionCte);
        //    testForm.SetField("codigo_cliente", oFactura.IdCliente.ToString());
        //    testForm.SetField("dni_cliente", oFactura.DniCliente);
        //    testForm.SetField("medio_pago", oFactura.MedioPago);
        //    testForm.SetField("fecha_entrega", "");
        //    testForm.SetField("nro_cliente", oFactura.IdCliente.ToString());
        //    testForm.SetField("nro_pedido", oFactura.NroPedido.ToString());
        //    testForm.SetField("total", oFactura.Total.ToString("c"));


        //    // HASTA 15
        //    for (int i = 0; i <= lstVehiculos.Count() - 1; i++)
        //    {
        //        // Datos de los vehiculos
        //        testForm.SetField("dominio_" + i.ToString(), lstVehiculos[i].Dominio);
        //        testForm.SetField("descripcion_" + i.ToString(), lstVehiculos[i].NombreModelo + " " + lstVehiculos[i].NombreMarca);
        //        testForm.SetField("cantidad_" + i.ToString(), "1");
        //        testForm.SetField("precio_" + i.ToString(), lstVehiculos[i].Precio.ToString("c"));
        //        testForm.SetField("dto_" + i.ToString(), "-");
        //        testForm.SetField("importe_" + i.ToString(), lstVehiculos[i].Precio.ToString("c"));
        //    }



        //    PdfContentByte overContent = pdfStamper.GetOverContent(1);

        //    pdfStamper.FormFlattening = true;

        //    pdfStamper.Close();

        //    pdfReader.Close();

        //    return "factura_" + oFactura.IdFactura + ".pdf";

        //}



    


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