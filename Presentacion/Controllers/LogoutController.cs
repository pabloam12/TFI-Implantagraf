using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;

namespace Presentacion.Controllers
{
    public class LogoutController : Controller
    {

        public ActionResult Index()
        {
            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;

            return RedirectToAction("Index", "Home");
        }

    }
}




