using Entidades;
using Negocio;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class RescateController : Controller
    {
        // GET: Rescate
        public ActionResult Index()
        {

            return View();

        }

        public ActionResult SolucionarIntegridad()
        {
            var integridad = new IntegridadDatos();

            integridad.RecalcularTodosDVH();

            integridad.LimpiarTablaRegistrosTablasFaltantes();

            integridad.ValidarIntegridadGlobal();

            return View();

        }

       
    }
}