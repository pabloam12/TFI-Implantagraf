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

            return View();
        }


        public ActionResult DetalleProducto(int productoId = 0)//int codProducto = 0)
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

                int idexistente = controlarId(productoId);

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

            int idProdCarrito = controlarId(productoId);

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

            int idProdCarrito = controlarId(productoId);

            productosCarrito[idProdCarrito].Cantidad++;

            Session["Carrito"] = productosCarrito;

            
            Session["ItemsCarrito"] = ((int)Session["ItemsCarrito"] + 1);

            
            return View("MostrarCarrito");


        }

        private int controlarId(int id)
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            for (int i = 0; i < productosCarrito.Count; i++)
            {
                if (productosCarrito[i].ProductoId == id)
                    return i;
            }

            return -1;
        }

        private int verCantidadProductos(int id)
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            for (int i = 0; i < productosCarrito.Count; i++)
            {
                if (productosCarrito[i].ProductoId == id)

                    return productosCarrito[i].Cantidad;
            }

            return -1;
        }

        private int restarProducto(int id)
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

            return View();

        }

        //[HttpPost]

        //TarjetaCredito datosTarjeta = null,
        public ActionResult RealizarPagoContado(int formaPago = 0)
        {
            var importeTotal = CalularImporteTotal();

            var fechaHora = DateTime.Now;

            //if (formaPago == 0)
            //{
            //    if (ValidarPago(datosTarjeta, importeTotal))
            //    { return View(); }
            //}

            RegistrarVenta(fechaHora, importeTotal, formaPago);

            //ActualizarStock();

            return RedirectToAction("FinalizarCompra");

        }

        private void RegistrarVenta(DateTime fechaHora, double importeTotal, int formaPago)
        {
            var ln = new NegocioOperaciones();

            var estado = "PAGO_PENDIENTE";

            if (formaPago == 0)

            { estado = "APROBADA"; }

            var factura = ln.RegistrarFactura(fechaHora, "A", importeTotal, formaPago, estado, (String)Session["DireccionUsuario"], (String)Session["RazonSocialUsuario"], (String)Session["EmailUsuario"]);

            var venta = ln.RegistrarVenta(fechaHora, (Int32)Session["CodCliente"],  importeTotal, formaPago, "VE", estado, factura.Codigo);

            RegistrarDetalleVenta(venta.Id);

            Session["Factura"] = factura;

            Session["Carrito"] = null;

        }

        private double CalularImporteTotal()
        {
            double importeTotal = 0;

            if (Session["Carrito"] != null)
            {
                foreach (var item in Session["Carrito"] as List<Carrito>)
                {
                    importeTotal += (double)(item.Precio * item.Cantidad);

                }
            }

            return importeTotal;
        }

        private void RegistrarDetalleVenta(int operacionId)
        {

            var ln = new NegocioOperaciones();

            if (Session["Carrito"] != null)
            {
                foreach (var item in Session["Carrito"] as List<Carrito>)
                {
                    var subtotal = (double)(item.Precio * item.Cantidad);

                    var detalleDVH = 23323;

                    ln.RegistrarDetalleOperacion(operacionId, item.ProductoId, item.Precio, item.Cantidad, subtotal, detalleDVH);

                }
            }

        }

        private bool ValidarPago(TarjetaCredito datosTarjeta, double importeTotal)
        {
            //var ws = new WebService();

            //if (ws.ValidarTarjeta(datosTarjeta.Numero, datosTarjeta.MesVenc, datosTarjeta.AnioVenc, datosTarjeta.CodigoV))
            //{
            //    Session["ErrorTarjeta"] = "La tarjeta ingresada no es válida.";
            //    return true;
            //}

            //if (ws.ValidarLimite(importeTotal))
            //{
            //    Session["ErrorTarjeta"] = "La tarjeta ingresada no cuenta con límite suficiente.";
            //    return true;
            //}

            return false;
        }

        public ActionResult FinalizarCompra()
        {
            return View();

        }

    }
}








