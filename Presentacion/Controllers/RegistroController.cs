using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class RegistroController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return RedirectToAction("Registrar");
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            var ln = new NegocioCuenta();

            var usrSesion = ln.Registrar(usuario);

            //ViewBag.Message = usrSesion.RazonSocial  + " se ha registrado correctamente.";

            return RedirectToLocal(null);

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
