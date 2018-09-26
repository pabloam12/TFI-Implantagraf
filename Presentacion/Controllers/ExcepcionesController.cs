using Negocio;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class ExcepcionesController : Controller
    {
        // GET: Excepciones
        public ActionResult Index()
        {
            var aud = new Auditoria();
            var inte = new IntegridadDatos();
            var ln = new NegocioCuenta();

            var criticidad = "LEVE";

            if ((String)Session["UsrLogin"] == null || (String)Session["UsrLogin"] == "")
            {
                Session["UsrLogin"] = "SISTEMA";
                criticidad = "GRAVE";
            }

            var BitacoraDVH = inte.CalcularDVH((String)Session["UsrLogin"] + "EXCEPCION" + criticidad);

            aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "EXCEPCIÓN", criticidad, (String)Session["Excepcion"], BitacoraDVH);

            ln.ActivarCuentaUsuario((String)Session["UsrLogin"]);

            Session["ErrorLogin"] = null;
            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            
            return View();
        }
    }
}