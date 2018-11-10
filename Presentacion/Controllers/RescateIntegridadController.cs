using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class RescateIntegridadController : Controller
    {
        // GET: RescateIntegridad
        public ActionResult Index()
        {
            
            var inte = new IntegridadDatos();

            return View(inte.ListarRegistrosTablasFaltantes());
            
        }
    }
}