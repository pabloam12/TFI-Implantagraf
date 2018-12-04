using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Seguridad;
using Servicios;
using System.IO;
using iTextSharp.text.pdf;

namespace Presentacion.Controllers
{
    public class ProductoController : Controller
    {
        public int[] productosValidos = { 110, 120, 130, 140, 150, 205, 210, 215, 225, 230, 245, 260, 265, 270, 280, 285, 450, 450, 460, 305, 315, 320, 330, 340, 350, 360, 365, 410, 420, 425, 440, 465, 470, 480, 485, 710, 720, 730, 740, 750, 760, 770, 810, 815, 820, 825 };
        public ActionResult Catalogo()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] != "WebMaster" && integ.ValidarExistencia("Producto") == 1)
            {

                TraducirPagina((String)Session["IdiomaApp"]);

                return View();

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetalleProducto(int productoId = 0)
        {
            if ((String)Session["PerfilUsuario"] != "WebMaster" && productosValidos.Contains(productoId) == true)
            {
                var ln = new NegocioProducto();

                TraducirPagina((String)Session["IdiomaApp"]);

                var producto = ln.BuscarPorId(productoId);

                ViewBag.CodigoProducto = producto.Codigo.ToString();

                ViewBag.Imagen = producto.Imagen;

                if ((String)Session["IdiomaApp"] == "Esp" || (String)Session["IdiomaApp"] == null)
                {
                    ViewBag.Titulo = producto.Titulo;
                    ViewBag.Descripcion = producto.Descripcion;
                }
                else
                {
                    ViewBag.Titulo = producto.Titulo_Eng;
                    ViewBag.Descripcion = producto.Descripcion_Eng;
                }

                ViewBag.Modelo = producto.Modelo;

                ViewBag.Precio = producto.Precio.ToString();


                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MostrarCarrito()
        {
            if ((String)Session["PerfilUsuario"] != "WebMaster")
            {
                TraducirPagina((String)Session["IdiomaApp"]);

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AgregarCarrito(int productoId = 0)
        {
            if ((String)Session["PerfilUsuario"] != "WebMaster" && productosValidos.Contains(productoId) == true)
            {
                var ln = new NegocioProducto();

                TraducirPagina((String)Session["IdiomaApp"]);

                var producto = ln.BuscarPorId(productoId);

                if (Session["Carrito"] == null)
                {
                    List<Carrito> productosCarrito = new List<Carrito>();

                    var carritoItem = new Carrito();

                    carritoItem.ProductoId = producto.Codigo;

                    if ((String)Session["IdiomaApp"] == "Esp" || (String)Session["IdiomaApp"] == null)
                    {
                        carritoItem.Descripcion = producto.Titulo + " - " + producto.Modelo;
                    }
                    else
                    {
                        carritoItem.Descripcion = producto.Titulo_Eng + " - " + producto.Modelo;
                    }

                    carritoItem.Cantidad = 1;
                    carritoItem.Precio = producto.Precio;

                    productosCarrito.Add(carritoItem);
                    Session["Carrito"] = productosCarrito;
                }
                else
                {
                    List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];
                    var carritoItem = new Carrito();
                    carritoItem.ProductoId = producto.Codigo;

                    if ((String)Session["IdiomaApp"] == "Esp" || (String)Session["IdiomaApp"] == null)
                    {
                        carritoItem.Descripcion = producto.Titulo + " - " + producto.Modelo;
                    }
                    else
                    {
                        carritoItem.Descripcion = producto.Titulo_Eng + " - " + producto.Modelo;
                    }

                    carritoItem.Cantidad = 1;
                    carritoItem.Precio = producto.Precio;

                    int idexistente = ControlarId(productoId);

                    if (idexistente == -1)
                        productosCarrito.Add(carritoItem);
                    else
                        productosCarrito[idexistente].Cantidad++;

                    Session["Carrito"] = productosCarrito;
                }

                Session["ItemsCarrito"] = ((int)Session["ItemsCarrito"] + 1);

                return RedirectToAction("MostrarCarrito");

            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult QuitarProductoCarrito(int productoId = 0)
        {
            if ((String)Session["PerfilUsuario"] != "WebMaster" && productosValidos.Contains(productoId) == true)
            {

                List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

                TraducirPagina((String)Session["IdiomaApp"]);

                int idProdCarrito = ControlarId(productoId);

                productosCarrito[idProdCarrito].Cantidad--;

                if (productosCarrito[idProdCarrito].Cantidad == 0)

                { productosCarrito.RemoveAt(idProdCarrito); }

                Session["Carrito"] = productosCarrito;

                //int cantidadTotalItems = (int)Session["ItemsCarrito"];

                Session["ItemsCarrito"] = ((int)Session["ItemsCarrito"] - 1);

                //cantidadTotalItems = (int)Session["ItemsCarrito"];

                return View("MostrarCarrito");

            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult SumarProductoCarrito(int productoId = 0)
        {
            if ((String)Session["PerfilUsuario"] != "WebMaster" && productosValidos.Contains(productoId) == true)
            {
                TraducirPagina((String)Session["IdiomaApp"]);

                List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

                int idProdCarrito = ControlarId(productoId);

                productosCarrito[idProdCarrito].Cantidad++;

                Session["Carrito"] = productosCarrito;


                Session["ItemsCarrito"] = ((int)Session["ItemsCarrito"] + 1);


                return View("MostrarCarrito");

            }

            return RedirectToAction("Index", "Home");

        }

        private int ControlarId(int id)
        {

            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            for (int i = 0; i < productosCarrito.Count; i++)
            {
                if (productosCarrito[i].ProductoId == id)
                    return i;
            }

            return -1;
        }

        private int VerCantidadProductos(int id)
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            for (int i = 0; i < productosCarrito.Count; i++)
            {
                if (productosCarrito[i].ProductoId == id)

                    return productosCarrito[i].Cantidad;
            }

            return -1;
        }

        private int RestarProducto(int id)
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            for (int i = 0; i < productosCarrito.Count; i++)
            {
                if (productosCarrito[i].ProductoId == id)

                    return (productosCarrito[i].Cantidad--);

            }

            return -1;

        }

        public ActionResult RealizarPago()
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            if (productosCarrito.Count > 0)
            {
                TraducirPagina((String)Session["IdiomaApp"]);

                if ((String)Session["PerfilUsuario"] != "Cliente" && (String)Session["PerfilUsuario"] != "Administrativo")
                { return RedirectToAction("Index", "Login"); }

                var ws = new WebService();


                var ln = new NegocioMarcaTC();

                ViewBag.Marcas_TC = ln.Listar();

                ViewBag.CUOTA_1 = "1 x $ " + ws.CalcularInteres(CalularImporteTotal(), 1).ToString() + ",00 .-";

                ViewBag.CUOTA_3 = "3 x $ " + ws.CalcularInteres(CalularImporteTotal(), 3).ToString() + ",00 .-";

                ViewBag.CUOTA_6 = "6 x $ " + ws.CalcularInteres(CalularImporteTotal(), 6).ToString() + ",00 .-";

                ViewBag.CUOTA_12 = "12 x $ " + ws.CalcularInteres(CalularImporteTotal(), 12).ToString() + ",00 .-";

                return View();

            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult RealizarPagoContado()
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            if (productosCarrito.Count > 0)
            {
                var importeTotal = CalularImporteTotal();
                var formaPago = 1;
                var fechaHora = DateTime.Now;

                var mensajeria = new Mensajeria();

                TraducirPagina((String)Session["IdiomaApp"]);

                RegistrarVenta(fechaHora, importeTotal, formaPago);

                //ActualizarStock();

                var facturaCompra = (Factura)Session["Factura"];


                return RedirectToAction("FinalizarCompra");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RealizarPagoTarjeta(FrmTarjetaCredito datosTarjeta)
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            if (productosCarrito.Count > 0)
            {
                var importeTotal = CalularImporteTotal();
                var formaPago = 2;
                var fechaHora = DateTime.Now;

                TraducirPagina((String)Session["IdiomaApp"]);

                var limiteOtorgado = ValidarTarjeta(datosTarjeta, importeTotal);

                if (limiteOtorgado == 0)
                {
                    Session["ErrorTarjetaCredito"] = ViewBag.ERROR_DATOS_TC_INVALIDOS;

                    return RedirectToAction("RealizarPago");
                }

                if (limiteOtorgado < importeTotal)
                {
                    Session["ErrorTarjetaCredito"] = ViewBag.ERROR_LIMITE_SALDO;

                    return RedirectToAction("RealizarPago");
                }

                RegistrarVenta(fechaHora, importeTotal, formaPago, datosTarjeta.Numero);


                return RedirectToAction("FinalizarCompra");

            }

            return RedirectToAction("Index", "Home");

        }

        private void RegistrarVenta(DateTime fechaHora, int importeTotal, int formaPago, string NroTarjeta = "N/A")
        {
            var ln = new NegocioOperaciones();
            var cliLn = new NegocioCliente();
            var inte = new IntegridadDatos();

            var mensajeria = new Mensajeria();

            TraducirPagina((String)Session["IdiomaApp"]);

            var estadoId = 2;// Estado PENDIENTE DE PAGO.

            if (formaPago == 2)

            { estadoId = 1; }// Estado APROBADA

            var tipoFactura = "A";

            var codUsuario = (Int32)Session["CodUsuario"];

            // Si existe el Cliente, lo traigo, sino lo doy de alta y luego lo traigo.
            var clienteActual = cliLn.TraerCliente(codUsuario);

            // Registro la Factura.
            var facturaActual = ln.RegistrarFactura(fechaHora, tipoFactura, importeTotal, formaPago, estadoId, clienteActual.Id);


            // Registro la Venta.
            var operacionActual = ln.RegistrarOperacion(fechaHora, clienteActual.Id, importeTotal, formaPago, "VE", estadoId, facturaActual.Codigo);

            // Registro Detalle de Venta.
            var detalleCompleto = RegistrarDetalleOperacion(operacionActual.Id);

            var rutaFactura = "";

            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];
            try
            {
                rutaFactura = GenerarFacturaPDF(facturaActual, productosCarrito);
            }
            catch
            {
                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR GENERAR FACTURA", "ERROR LEVE", "Fallo al intentar generar la factura de venta.");

            }
            // Me guardo la factura para imprimir y enviar por correo.
            Session["Factura"] = facturaActual;


            try
            {
                var cuerpoMsj = ViewBag.MENSAJE_MAIL_COMPRA;
                var asuntoMsj = "F-000" + facturaActual.Codigo.ToString();
                mensajeria.EnviarCorreo("implantagraf@gmail.com", (String)Session["EmailUsuario"], asuntoMsj, cuerpoMsj, rutaFactura);
            }
            catch
            {
                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR ENVÍO EMAIL", "ERROR LEVE", "Fallo al intentar enviar la factura por correo.");

            }

            //Borro los items del Carrito.
           Session["Carrito"] = null;

        }

        
        public String GenerarFacturaPDF(Factura oFactura, List<Carrito> productosCarrito)
        {

            //string pdfTemplate = @Server.MapPath("~/Documentos/factura_" + oFactura.Codigo.ToString() + ".pdf");

            string pdfTemplate = "C:\\Implantagraf\\PDF\\F_" + oFactura.Codigo.ToString() + ".pdf";

            PdfReader pdfReader = null;

            // Create the form filler
            FileStream pdfOutputFile = new FileStream(pdfTemplate, FileMode.Create);

            if ((String)Session["IdiomaApp"] == "Esp" || (String)Session["IdiomaApp"] == null)
            { pdfReader = new PdfReader(@Server.MapPath("~/Documentos/e-factura.pdf")); }

            if ((String)Session["IdiomaApp"] == "Eng")
            { pdfReader = new PdfReader(@Server.MapPath("~/Documentos/e-factura-eng.pdf")); }

            PdfStamper pdfStamper = null;

            pdfStamper = new PdfStamper(pdfReader, pdfOutputFile);

            // Get the form fields
            AcroFields testForm = pdfStamper.AcroFields;

            // Datos de la factura
            if ((String)Session["IdiomaApp"] == "Eng")
            { testForm.SetField("factura", "000-0" + oFactura.Codigo.ToString() + "        Date: " + oFactura.FechaHora.ToShortDateString()); }

            else { testForm.SetField("factura", "000-0" + oFactura.Codigo.ToString() + "        Fecha: " + oFactura.FechaHora.ToShortDateString()); }

            testForm.SetField("tipo_factura", " B");
            testForm.SetField("pagina_de", "1");
            testForm.SetField("pagina_hta", "1");
            testForm.SetField("nombre_cliente", oFactura.Cliente.RazonSocial);
            testForm.SetField("direccion_cliente", "Direccion: " + oFactura.Cliente.Direccion);
            testForm.SetField("codigo_cliente", oFactura.Cliente.Id.ToString());
            testForm.SetField("dni_cliente", oFactura.Cliente.CUIL);
            if ((String)Session["IdiomaApp"] == "Eng" && oFactura.FormaPago.Id == 1)
            { testForm.SetField("medio_pago", "CASH"); }

            if ((String)Session["IdiomaApp"] == "Eng" && oFactura.FormaPago.Id == 2)
            { testForm.SetField("medio_pago", "CREDIT CARD"); }

            if ((String)Session["IdiomaApp"] == "Esp" || (String)Session["IdiomaApp"] == null)
            {
                testForm.SetField("medio_pago", oFactura.FormaPago.Descripcion);
            }
            testForm.SetField("fecha_entrega", "");
            testForm.SetField("nro_cliente", oFactura.Cliente.Id.ToString());
            testForm.SetField("nro_pedido", oFactura.Codigo.ToString());
            testForm.SetField("total", "$ " + oFactura.Monto.ToString() + ".-");


            // HASTA 15
            for (int i = 0; i <= productosCarrito.Count() - 1; i++)
            {
                var subTotal = productosCarrito[i].Precio * productosCarrito[i].Cantidad;

                // Datos de los productos 
                testForm.SetField("dominio_" + i.ToString(), productosCarrito[i].ProductoId.ToString());
                testForm.SetField("descripcion_" + i.ToString(), productosCarrito[i].Descripcion);
                testForm.SetField("cantidad_" + i.ToString(), productosCarrito[i].Cantidad.ToString());
                testForm.SetField("precio_" + i.ToString(), "$ " + productosCarrito[i].Precio.ToString() + ".-");
                testForm.SetField("dto_" + i.ToString(), "-");
                testForm.SetField("importe_" + i.ToString(), "$ " + subTotal.ToString() + ".-");
            }



            PdfContentByte overContent = pdfStamper.GetOverContent(1);

            pdfStamper.FormFlattening = true;

            pdfStamper.Close();

            pdfReader.Close();

            return "C:\\Implantagraf\\PDF\\F_" + oFactura.Codigo.ToString() + ".pdf";

        }

        private int CalularImporteTotal()
        {
            int importeTotal = 0;

            if (Session["Carrito"] != null)
            {
                foreach (var item in Session["Carrito"] as List<Carrito>)
                {
                    importeTotal += (item.Precio * item.Cantidad);

                }
            }

            return importeTotal;
        }

        private List<DetalleOperacion> RegistrarDetalleOperacion(int operacionId)
        {

            var ln = new NegocioOperaciones();
            var inte = new IntegridadDatos();


            if (Session["Carrito"] != null)
            {
                var detalleCompleto = new List<DetalleOperacion>();

                foreach (var item in Session["Carrito"] as List<Carrito>)
                {
                    var subtotal = (item.Precio * item.Cantidad);


                    var detalleActual = new DetalleOperacion
                    {
                        OperacionId = operacionId,
                        ProductoId = item.ProductoId,
                        Monto = item.Precio,
                        Cantidad = item.Cantidad,
                        SubTotal = subtotal,

                    };

                    detalleCompleto.Add(detalleActual);

                    ln.RegistrarDetalleOperacion(detalleActual);

                    detalleActual.DVH = inte.CalcularDVH(detalleActual.OperacionId.ToString() + detalleActual.ProductoId.ToString() + detalleActual.SubTotal.ToString() + detalleActual.Cantidad.ToString() + detalleActual.Monto.ToString());

                    // Actualiza el DVH
                    inte.ActualizarDVHDetalleOperacion(detalleActual.OperacionId, detalleActual.ProductoId, detalleActual.DVH);

                }

                inte.RecalcularDVV("DetalleOperacion");

                return detalleCompleto;
            }

            return null;

        }

        private long ValidarTarjeta(FrmTarjetaCredito datosTarjeta, int importeTotal)
        {
            var ws = new WebService();

            var limite = ws.ValidarTarjeta(datosTarjeta.Numero, datosTarjeta.Marca_TC.Id, datosTarjeta.MesVenc, datosTarjeta.AnioVenc, datosTarjeta.CodigoV);

            return limite;
        }

        public ActionResult FinalizarCompra()
        {

            TraducirPagina((String)Session["IdiomaApp"]);

            return View();

        }


        private void TraducirPagina(string idioma)
        {
            var traductor = new Traductor();

            // Buscar Traducciones de Idioma.
            if (idioma == null)
            { idioma = "Esp"; }

            //Devuelve el Hastable con todas las traducciones.
            var diccionario = traductor.Traducir(idioma);

            //Traduce Vista PRODUCTOS.

            ViewBag.PRODUCTO_CATALOGO_TITULO = diccionario["PRODUCTO_CATALOGO_TITULO"];
            ViewBag.CATALOGO_SELECTOR_TODAS = diccionario["CATALOGO_SELECTOR_TODAS"];
            ViewBag.CATALOGO_SELECTOR_MANUALES = diccionario["CATALOGO_SELECTOR_MANUALES"];
            ViewBag.CATALOGO_SELECTOR_SEMIAUTO = diccionario["CATALOGO_SELECTOR_SEMIAUTO"];
            ViewBag.CATALOGO_SELECTOR_AUTO = diccionario["CATALOGO_SELECTOR_AUTO"];
            ViewBag.CATALOGO_SELECTOR_TAMPO = diccionario["CATALOGO_SELECTOR_TAMPO"];
            ViewBag.CATALOGO_SELECTOR_OTRAS = diccionario["CATALOGO_SELECTOR_OTRAS"];

            ViewBag.PRODUCTO_IMPRESORA_MANUAL = diccionario["PRODUCTO_IMPRESORA_MANUAL"];
            ViewBag.BOTON_VER = diccionario["BOTON_VER"];

            ViewBag.PRODUCTO_IMPRESORA_SEMIAUTOMATICA = diccionario["PRODUCTO_IMPRESORA_SEMIAUTOMATICA"];
            ViewBag.PRODUCTO_IMPRESORA_AUTOMATICA = diccionario["PRODUCTO_IMPRESORA_AUTOMATICA"];
            ViewBag.PRODUCTO_IMPRESORA_TAMPOGRAFICA = diccionario["PRODUCTO_IMPRESORA_TAMPOGRAFICA"];
            ViewBag.PRODUCTO_TUNEL_SECADO = diccionario["PRODUCTO_TUNEL_SECADO"];
            ViewBag.PRODUCTO_LAMPARA_UV = diccionario["PRODUCTO_LAMPARA_UV"];

            ViewBag.PRODUCTO_DETALLE_TITULO = diccionario["PRODUCTO_DETALLE_TITULO"];
            ViewBag.PRODUCTO_LEYENDA_ENVIO = diccionario["PRODUCTO_LEYENDA_ENVIO"];
            ViewBag.BOTON_AGREGAR_CARRITO = diccionario["BOTON_AGREGAR_CARRITO"];

            ViewBag.PRODUCTO_FINALIZAR_COMPRA_LEYENDA = diccionario["PRODUCTO_FINALIZAR_COMPRA_LEYENDA"];
            ViewBag.BOTON_FINALIZAR = diccionario["BOTON_FINALIZAR"];

            ViewBag.PRODUCTO_CARRITO_TITULO = diccionario["PRODUCTO_CARRITO_TITULO"];
            ViewBag.PRODUCTO_CARRITO_LEYENDA_SIN_PRODUCTOS = diccionario["PRODUCTO_CARRITO_LEYENDA_SIN_PRODUCTOS"];
            ViewBag.ENTIDAD_FECHA = diccionario["ENTIDAD_FECHA"];
            ViewBag.BOTON_COMPRAR = diccionario["BOTON_COMPRAR"];
            ViewBag.ENTIDAD_CODIGO = diccionario["ENTIDAD_CODIGO"];
            ViewBag.ENTIDAD_DESCRIPCION = diccionario["ENTIDAD_DESCRIPCION"];
            ViewBag.ENTIDAD_CANTIDAD = diccionario["ENTIDAD_CANTIDAD"];


            ViewBag.ENTIDAD_PRECIO_UNITARIO = diccionario["ENTIDAD_PRECIO_UNITARIO"];
            ViewBag.ENTIDAD_SUBTOTAL = diccionario["ENTIDAD_SUBTOTAL"];
            ViewBag.BOTON_QUITAR_PRODUCTO = diccionario["BOTON_QUITAR_PRODUCTO"];

            ViewBag.ENTIDAD_IMPORTE_TOTAL_PAGAR = diccionario["ENTIDAD_IMPORTE_TOTAL_PAGAR"];
            ViewBag.BOTON_AGREGAR_CARRITO = diccionario["BOTON_AGREGAR_CARRITO"];

            ViewBag.PRODUCTO_PAGO_TC_TITULO = diccionario["PRODUCTO_PAGO_TC_TITULO"];
            ViewBag.ENTIDAD_TITULAR = diccionario["ENTIDAD_TITULAR"];
            ViewBag.ENTIDAD_NUMERO_TC = diccionario["ENTIDAD_NUMERO_TC"];
            ViewBag.ENTIDAD_EMPRESA_TC = diccionario["ENTIDAD_EMPRESA_TC"];
            ViewBag.ENTIDAD_MES_V = diccionario["ENTIDAD_MES_V"];
            ViewBag.ENTIDAD_ANIO_V = diccionario["ENTIDAD_ANIO_V"];

            ViewBag.ENTIDAD_CODIGO_SEG = diccionario["ENTIDAD_CODIGO_SEG"];
            ViewBag.BOTON_COMPRAR = diccionario["BOTON_COMPRAR"];
            ViewBag.PRODUCTO_PAGO_CONTADO_TITULO = diccionario["PRODUCTO_PAGO_CONTADO_TITULO"];


            ViewBag.ERROR_LIMITE_SALDO = diccionario["ERROR_LIMITE_SALDO"];
            ViewBag.ERROR_DATOS_TC_INVALIDOS = diccionario["ERROR_DATOS_TC_INVALIDOS"];
            ViewBag.MENSAJE_MAIL_COMPRA = diccionario["MENSAJE_MAIL_COMPRA"];

            ViewBag.PRODUCTO_PAGO_CONTADO_LEYENDA = diccionario["PRODUCTO_PAGO_CONTADO_LEYENDA"];

            ViewBag.ENTIDAD_CUOTAS = diccionario["ENTIDAD_CUOTAS"];

            ViewBag.PRODUCTO_LEYENDA_NUEVO = diccionario["PRODUCTO_LEYENDA_NUEVO"];


        }

    }
}








