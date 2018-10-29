﻿using System;
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
            return View();
        }

        public ActionResult DetalleProducto(int productoId = 0)
        {
            var ln = new NegocioProducto();

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
            return View();
        }

        public ActionResult AgregarCarrito(int productoId = 0)
        {
            var ln = new NegocioProducto();

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
            if ((String)Session["PerfilUsuario"] != "Cliente" && (String)Session["PerfilUsuario"] != "Administrativo")
            { return RedirectToAction("Index", "Login"); }

            var ln = new NegocioMarcaTC();

            ViewBag.Marcas_TC = ln.Listar();

            return View();

        }

        public ActionResult RealizarPagoContado()
        {
            var importeTotal = CalularImporteTotal();
            var formaPago = 1;
            var fechaHora = DateTime.Now;

            var mensajeria = new Mensajeria();

            RegistrarVenta(fechaHora, importeTotal, formaPago);

            //ActualizarStock();

            var facturaCompra = (Factura)Session["Factura"];

            // Envío correo con Factura adjunta TODO.
            var cuerpoMsj = "Muchas gracias por su compra! Adjuntamos su factura y recuerde que para recibir la mercaderia debera abonar el total de la factura en nuestras oficinas comerciales. Saludos.";
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

            var limiteOtorgado = ValidarTarjeta(datosTarjeta, importeTotal);

            if (limiteOtorgado == 0)
            {
                Session["ErrorTarjetaCredito"] = "Datos de Tarjeta inválidos.";

                return RedirectToAction("RealizarPago");
            }

            if (limiteOtorgado < importeTotal)
            {
                Session["ErrorTarjetaCredito"] = "Límite de saldo de compra insuficiente.";

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
            return View();

        }

    }
}








