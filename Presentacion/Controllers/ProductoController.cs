using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Seguridad;

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
            ViewBag.Imagen = "../../Imagenes/Fotos Maquinas/1.Manuales/MT-110.jpg";
            ViewBag.Titulo = "Impresora Manual";
            ViewBag.Modelo = "MC-110";
            ViewBag.Descripcion = "Máquina manual para imprimir en telas. Máxima área de impresión 150x100mm.";
            ViewBag.Precio = "23.200";
            ViewBag.CodigoProducto = productoId;

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
                carritoItem.ProductoId = producto.Id;
                carritoItem.Descripcion = producto.Nombre;
                carritoItem.Cantidad = 1;
                carritoItem.Precio = producto.Precio;

                productosCarrito.Add(carritoItem);
                Session["Carrito"] = productosCarrito;
            }
            else
            {
                List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];
                var carritoItem = new Carrito();
                carritoItem.ProductoId = producto.Id;
                carritoItem.Descripcion = producto.Nombre;
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

            return View();
        }

        public ActionResult QuitarProductoCarrito(int productoId)
        {
            List<Carrito> productosCarrito = (List<Carrito>)Session["Carrito"];

            int idProdCarrito = controlarId(productoId);

            productosCarrito[idProdCarrito].Cantidad--;

            if (productosCarrito[idProdCarrito].Cantidad == 0)

            { productosCarrito.RemoveAt(idProdCarrito); }

            Session["Carrito"] = productosCarrito;

            int cantidadTotalItems = (int)Session["ItemsCarrito"];

            Session["ItemsCarrito"] = ((int)Session["ItemsCarrito"] - 1);

            cantidadTotalItems = (int)Session["ItemsCarrito"];

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

    }
}








