using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Seguridad;
using Negocio;

namespace Presentacion.Controllers
{
    public class LogoutController : Controller
    {

        public ActionResult Index()
        {
            var aud = new Auditoria();
            
            var ln = new NegocioCuenta();

                        aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "CIERRE DE SESIÓN", "INFO", "El Usuario ha cerrado sesión.");

            //ln.ActivarCuentaUsuario((String)Session["UsrLogin"]);

            Session["ItemsCarrito"] = 0;

            Session["ErrorLogin"] = null;
            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;
            Session["DireccionUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            Session["Excepcion"] = null;
     
            return RedirectToAction("Index", "Home");
        }

    }
}




