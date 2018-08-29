using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class BitacoraController : Controller
    {

        // GET: Idioma
        public ActionResult Index()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                var ln = new NegocioBitacora();

                return View(ln.ConsultarBitacora());
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
