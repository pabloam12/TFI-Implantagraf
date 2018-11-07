using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Seguridad;
using Servicios;

namespace Presentacion.Controllers
{
    public class ProductoController : Controller
    {

        public ActionResult Catalogo()
        {
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();
        }

        public ActionResult DetalleProducto(int productoId = 0)
        {
            var ln = new NegocioProducto();

            TraducirPagina((String)Session["IdiomaApp"]);

            var producto = ln.BuscarPorId(productoId);

            ViewBag.CodigoProducto = producto.Codigo.ToString();

            ViewBag.Imagen = producto.Imagen;
            ViewBag.Titulo = producto.Titulo;
            ViewBag.Modelo = producto.Modelo;
            ViewBag.Descripcion = producto.Descripcion;
            ViewBag.Precio = producto.Precio.ToString();


            return View();
        }

        public ActionResult MostrarCarrito()
        {
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();
        }

        public ActionResult AgregarCarrito(int productoId = 0)
        {
            var ln = new NegocioProducto();

            TraducirPagina((String)Session["IdiomaApp"]);

            var producto = ln.BuscarPorId(productoId);

            if (Session["Carrito"] == null)
            {
                List<Carrito> productosCarrito = new List<Carrito>();

                var carritoItem = new Carrito();
                carritoItem.ProductoId = producto.Codigo;
                carritoItem.Descripcion = producto.Titulo + " - " + producto.Modelo;
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
                carritoItem.Descripcion = producto.Titulo + " - " + producto.Modelo;
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

        public ActionResult QuitarProductoCarrito(int productoId)
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

        public ActionResult SumarProductoCarrito(int productoId)
        {
            TraducirPagina((String)Session["IdiomaApp"]);

            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            int idProdCarrito = ControlarId(productoId);

            productosCarrito[idProdCarrito].Cantidad++;

            Session["Carrito"] = productosCarrito;


            Session["ItemsCarrito"] = ((int)Session["ItemsCarrito"] + 1);


            return View("MostrarCarrito");


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
            var ws = new WebService();

            TraducirPagina((String)Session["IdiomaApp"]);

            if ((String)Session["PerfilUsuario"] != "Cliente" && (String)Session["PerfilUsuario"] != "Administrativo")
            { return RedirectToAction("Index", "Login"); }

            var ln = new NegocioMarcaTC();

            ViewBag.Marcas_TC = ln.Listar();

            ViewBag.CUOTA_1 = "1 x $ " + ws.CalcularInteres(CalularImporteTotal(), 1).ToString() + ",00 .-";

            ViewBag.CUOTA_3 = "3 x $ " + ws.CalcularInteres(CalularImporteTotal(), 3).ToString() + ",00 .-";

            ViewBag.CUOTA_6 = "6 x $ " + ws.CalcularInteres(CalularImporteTotal(), 6).ToString() + ",00 .-";

            ViewBag.CUOTA_12 = "12 x $ " + ws.CalcularInteres(CalularImporteTotal(), 12).ToString() + ",00 .-";

            return View();

        }

        public ActionResult RealizarPagoContado()
        {
            var importeTotal = CalularImporteTotal();
            var formaPago = 1;
            var fechaHora = DateTime.Now;

            var mensajeria = new Mensajeria();

            TraducirPagina((String)Session["IdiomaApp"]);

            RegistrarVenta(fechaHora, importeTotal, formaPago);

            //ActualizarStock();

            var facturaCompra = (Factura)Session["Factura"];

            // Envío correo con Factura adjunta TODO.
            var cuerpoMsj = ViewBag.MENSAJE_MAIL_COMPRA;
            var asuntoMsj = "Factura " + facturaCompra.Codigo.ToString();
            mensajeria.EnviarCorreo("implantagraf@gmail.com", (String)Session["EmailUsuario"], asuntoMsj, cuerpoMsj);

            return RedirectToAction("FinalizarCompra");

        }

        [HttpPost]
        public ActionResult RealizarPagoTarjeta(FrmTarjetaCredito datosTarjeta)
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

            //TODO
            //ActualizarStock();

            return RedirectToAction("FinalizarCompra");

        }

        private void RegistrarVenta(DateTime fechaHora, int importeTotal, int formaPago, string NroTarjeta = "N/A")
        {
            var ln = new NegocioOperaciones();
            var cliLn = new NegocioCliente();
            var inte = new IntegridadDatos();

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
            RegistrarDetalleOperacion(operacionActual.Id);



            // Me guardo la factura para imprimir y enviar por correo.
            Session["Factura"] = facturaActual;

            // Borro los items del Carrito.
            Session["Carrito"] = null;

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

        private void RegistrarDetalleOperacion(int operacionId)
        {

            var ln = new NegocioOperaciones();
            var inte = new IntegridadDatos();


            if (Session["Carrito"] != null)
            {
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

                    ln.RegistrarDetalleOperacion(detalleActual);

                    detalleActual.DVH = inte.CalcularDVH(detalleActual.OperacionId.ToString() + detalleActual.ProductoId.ToString() + detalleActual.SubTotal.ToString() + detalleActual.Cantidad.ToString() + detalleActual.Monto.ToString());

                    // Actualiza el DVH
                    inte.ActualizarDVHDetalleOperacion(detalleActual.OperacionId, detalleActual.ProductoId, detalleActual.DVH);

                }

                inte.RecalcularDVV("DetalleOperacion");
            }

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


        }

    }
}








