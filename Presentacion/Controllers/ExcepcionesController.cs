using Negocio;
using Seguridad;
using Servicios;
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
            
            var ln = new NegocioCuenta();

            TraducirPagina((String)Session["IdiomaApp"]);

            var criticidad = "LEVE";

            if ((String)Session["UsrLogin"] == null || (String)Session["UsrLogin"] == "")
            {
                Session["UsrLogin"] = "SISTEMA";
                criticidad = "GRAVE";
            }

            var aud = new Auditoria();
            aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "EXCEPCIÓN", criticidad, (String)Session["Excepcion"]);

            ln.ActivarCuentaUsuario((String)Session["UsrLogin"]);

            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;
            Session["CodUsuario"] = null;
            Session["DireccionUsuario"] = null;
            Session["RazonSocialUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            
            return View();
        }


        private void TraducirPagina(string idioma)
        {
            var traductor = new Traductor();

            // Buscar Traducciones de Idioma.
            if (idioma == null)
            { idioma = "Esp"; }

            //Devuelve el Hastable con todas las traducciones.
            var diccionario = traductor.Traducir(idioma);

            //Traduce Vistas CUENTA.
            ViewBag.EXCEPCIONES_INDEX_TITULO = diccionario["EXCEPCIONES_INDEX_TITULO"];
        }
    }
}