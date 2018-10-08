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

        

    }
}








