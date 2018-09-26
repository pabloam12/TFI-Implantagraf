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
            Session["Excepcion"] = null;

            // Se comprueba la integridad de la base.
            var integridad = new IntegridadDatos();

            if (integridad.ValidarIntegridadTablas())
            { //TODO MENSAJE correcto
                Session["Excepcion"] = "[Error Nº 1] - Error de Integridad en la Base de Datos.";
                return RedirectToAction("Index", "Excepciones");
            }

            //Se comprueban los DVV
            if (integridad.ValidarTablasDVV())
            { //TODO MENSAJE correcto
                Session["Excepcion"] = "[Error Nº 2] - Error de Integridad en la Base de Datos.";
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