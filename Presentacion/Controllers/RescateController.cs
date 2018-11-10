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
        public int claveValida = 1601;
        // GET: Rescate
        public ActionResult Index(int clave = 0)
        {
            //TODO
                      
            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                return RedirectToAction("Index", "RescateIntegridad");
            }

            if (clave == claveValida)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult SolucionarIntegridad()
        {
            var integridad = new IntegridadDatos();

            integridad.RecalcularTodosDVH();
            
            integridad.LimpiarTablaRegistrosTablasFaltantes();

            integridad.ValidarIntegridadGlobal();

            return View();

        }

        public ActionResult RealizarBackUp()
        {
            var integridad = new IntegridadDatos();

            integridad.RealizarBackUp();

            return View();

        }

        public ActionResult ListarRespaldos()
        {
            var integridad = new IntegridadDatos();


            return View(integridad.ListarRespaldos());

        }

        public ActionResult RestaurarCopiaRespaldo(string rutaCompleta)
        {
                    
                var integridad = new IntegridadDatos();

                integridad.RestaurarCopiaRespaldo(rutaCompleta);

                return View();
           
        }
    }
}