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


        public ActionResult DetalleProductoMC110()
        {
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

            return View();
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
    }
}








