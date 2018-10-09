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


        public ActionResult DetalleProducto()
        {

            return View();
        }

        public ActionResult AgregarCarrito(int productoId = 0, int cantidad = 0)
        {
            //var producto = new Producto ( productoId , "Maquina", "Maquina");

            if (Session["Carrito"] == null)
            {
                List<Producto> cartItem = new List<Producto>();
                var carritoItem = new Producto();
                carritoItem.Id = productoId;
                carritoItem.Nombre = "Nombre de Prueba";
                carritoItem.Descripcion = "Descripcion de Prueba";
                //carritoItem.Quantity = producto.QuantitySold;

                cartItem.Add(carritoItem);
                Session["Carrito"] = cartItem;
            }
            else
            {
                List<Producto> cartItem = new List<Producto>();
                var carritoItem = new Producto();
                carritoItem.Id = productoId;
                carritoItem.Nombre = "Nombre de Prueba";
                carritoItem.Descripcion = "Descripcion de Prueba";
                //carritoItem.Quantity = producto.QuantitySold;

                cartItem.Add(carritoItem);

                //if (idexistente == -1)
                //    cartItem.Add(carritoItem);
                //else
                //    cartItem[idexistente].Quantity++;
                Session["Carrito"] = cartItem;
            }
            return View();
        }
    }
}








