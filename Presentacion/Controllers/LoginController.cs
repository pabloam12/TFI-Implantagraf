using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var ln = new NegocioCuenta();

            var usrSesion = ln.Autenticar(usuario);

            if (usrSesion.Nombre != null && usrSesion.Perfil.Descripcion != null)
            {
                Session["IdUsuario"] = usrSesion.Id.ToString();
                Session["NombreUsuario"] = usrSesion.Nombre.ToString();
                Session["PerfilUsuario"] = usrSesion.Perfil.Descripcion.ToString();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["IdUsuario"] = null;
                Session["NombreUsuario"] = null;
                Session["PerfilUsuario"] = null;
                return RedirectToAction("Login");
            }
        }
    }
}




