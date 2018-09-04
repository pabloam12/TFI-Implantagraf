using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class RegistroClienteController : Controller
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

            Session["ErrorRegistro"] = null;
            Session["Excepcion"] = null;
            // Usuario existente, solo devuelvo el error.
            if (ln.ValidarUsuario(usuario.Email) == false)
            {
                Session["ErrorRegistro"] = "Usuario ya existente.";
                return RedirectToAction("Registrarse");
            }

            var usrSesion = ln.RegistrarCliente(usuario);

            if (usrSesion.Nombre != "" && usrSesion.PerfilUsr.Descripcion != "")
            {
                Session["IdUsuario"] = usrSesion.Id.ToString();
                Session["NombreUsuario"] = usrSesion.Nombre.ToString();
                Session["PerfilUsuario"] = usrSesion.PerfilUsr.Descripcion.ToString();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //TODO.
                Session["Excepcion"] = "[Error Nº 47] - Error de Base de Datos";
                return RedirectToAction("Index", "Excepciones");
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
