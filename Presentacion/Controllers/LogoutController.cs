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
            Session["ErrorLogin"] = null;
            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            Session["Excepcion"] = null;

            return RedirectToAction("Index", "Home");
        }

    }
}




