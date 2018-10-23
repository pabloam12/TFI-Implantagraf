using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var integridad = new IntegridadDatos();

            integridad.LimpiarTablaRegistrosTablasFaltantes();

            Session["Excepcion"] = null;

            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                // Se comprueba la integridad de la base.
                integridad.ValidarIntegridadGlobal();

                return RedirectToAction("Index", "RescateIntegridad");
            }

            
            // Se comprueba la integridad de la base.
            if (integridad.ValidarIntegridadGlobal())
            { //TODO MENSAJE correcto
                Session["Excepcion"] = "[Error Nº 1] - Error de Integridad en la Base de Datos.";
                return RedirectToAction("Index", "Excepciones");
            }
       

            return View();
        }

        
        public ActionResult Informacion()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contacto()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}