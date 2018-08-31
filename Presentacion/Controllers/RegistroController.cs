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
            return RedirectToAction("Registrarse");
        }

        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(Usuario usuario)
        {
            var ln = new NegocioCuenta();

            var usrSesion = ln.RegistrarCliente(usuario);

            if (usrSesion.Nombre != null && usrSesion.Perfil.Descripcion != null)
            {
                Session["IdUsuario"] = usrSesion.Id.ToString();
                Session["NombreUsuario"] = usrSesion.Nombre.ToString();
                Session["PerfilUsuario"] = usrSesion.Perfil.Descripcion.ToString();

                return View(); // LoggedIn
            }
            else
            {
                Session["IdUsuario"] = null;
                Session["NombreUsuario"] = null;
                Session["PerfilUsuario"] = null;

                return RedirectToAction("RedirectToLocal");
            }

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
