using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Seguridad;
using Servicios;

namespace Presentacion.Controllers
{
    public class BitacoraController : Controller
    {
                
        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Bitacora") == 1)
            {
                var ln = new Auditoria();

                //Traducir Página BITACORA.
                TraducirPagina((String)Session["IdiomaApp"]);

                try
                {
                    var consulta = ln.ConsultarBitacora();
                    Session["ConsultaBitacora"] = consulta;

                    return View(consulta);
                }
                catch
                {
                    var aud = new Auditoria();
                    aud.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR BITÁCORA", "ERROR LEVE", "Error al consultar la bitácora.");
                    return RedirectToAction("Index", "Home");
                }


            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string fecha, string fechaFin, string usr, string accion, string criticidad)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Bitacora") == 1)
            {
                var ln = new Auditoria();

                //Traducir Página BITACORA.
                TraducirPagina((String)Session["IdiomaApp"]);

                Session["ErrorFiltroBitacora"] = null;

                if (fecha == "" && fechaFin != "")
                {
                    fechaFin = "";

                    Session["ErrorFiltroBitacora"] = ViewBag.BITACORA_WARNING_SIN_FECHA_INICIO;
                }

                if (fecha != "" && fechaFin != "")
                {
                    DateTime fechaDate = DateTime.Parse(fecha);

                    DateTime fechaFinDate = DateTime.Parse(fechaFin);

                    if (fechaDate >= fechaFinDate)
                    {
                        fecha = "";
                        fechaFin = "";

                        Session["ErrorFiltroBitacora"] = ViewBag.BITACORA_WARNING_FECHAS_MAL;

                    }

                }

                if (fecha == "" && usr == "" && accion == "" && criticidad == "")
                {
                    return View(ln.ConsultarBitacora());

                }

                var consulta = ln.ConsultarBitacora(fecha, fechaFin, usr, accion, criticidad);

                Session["ConsultaBitacora"] = consulta;

                return View(consulta);
            }



            return RedirectToAction("Index", "Home");
        }

        public ActionResult BitacoraHistorica()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Bitacora") == 1)
            {
                var ln = new Auditoria();

                //Traducir Página BITACORA.
                TraducirPagina((String)Session["IdiomaApp"]);
                try
                {                    
                    var consulta = new List<Bitacora>();
                    Session["ConsultaBitacora"] = consulta;

                    return View(consulta);
                }
                catch
                {
                    var aud = new Auditoria();
                    aud.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR BITACORA", "ERROR LEVE", "Error al intentar consultar la bitácora histórica.");
                    return RedirectToAction("Index", "Home");
                }


            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult BitacoraHistorica(string fecha, string fechaFin, string usr, string accion, string criticidad)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Bitacora") == 1)
            {
                var ln = new Auditoria();

                //Traducir Página BITACORA.
                TraducirPagina((String)Session["IdiomaApp"]);

                Session["ErrorFiltroBitacora"] = null;

                if (fecha == "" && fechaFin != "")
                {
                    fechaFin = "";

                    Session["ErrorFiltroBitacora"] = ViewBag.BITACORA_WARNING_SIN_FECHA_INICIO;
                }

                if (fecha != "" && fechaFin != "")
                {
                    DateTime fechaDate = DateTime.Parse(fecha);

                    DateTime fechaFinDate = DateTime.Parse(fechaFin);

                    if (fechaDate >= fechaFinDate)
                    {
                        fecha = "";
                        fechaFin = "";

                        Session["ErrorFiltroBitacora"] = ViewBag.BITACORA_WARNING_FECHAS_MAL;

                    }

                }

                if (fecha == "" && fechaFin == "" )
                {
                    var consultaVacia = new List<Bitacora>();
                    Session["ConsultaBitacora"] = consultaVacia;

                    return View(consultaVacia);
                }

                var consulta = ln.ConsultarBitacoraHistorica(fecha, fechaFin, usr, accion, criticidad);

                Session["ConsultaBitacora"] = consulta;

                return View(consulta);
            }



            return RedirectToAction("Index", "Home");
        }

        public ActionResult ExportarXML()
        {
            var exportador = new Exportador();

            List<Bitacora> consultaBitacora = (List<Bitacora>)Session["ConsultaBitacora"];

            exportador.ExportarBitacoraXML(consultaBitacora);

            return RedirectToAction("Index");
        }

        private void TraducirPagina(string idioma)
        {
            var traductor = new Traductor();

            // Buscar Traducciones de Idioma.
            if (idioma == null)
            { idioma = "Esp"; }

            //Devuelve el Hastable con todas las traducciones.
            var diccionario = traductor.Traducir(idioma);

            //Traduce Vista BITACORA.
            ViewBag.BITACORA_TITULO = diccionario["BITACORA_TITULO"];
            ViewBag.ENTIDAD_FECHA_INICIO = diccionario["ENTIDAD_FECHA_INICIO"];
            ViewBag.ENTIDAD_FECHA_FIN = diccionario["ENTIDAD_FECHA_FIN"];
            ViewBag.ENTIDAD_USUARIO = diccionario["ENTIDAD_USUARIO"];
            ViewBag.ENTIDAD_ACCION = diccionario["ENTIDAD_ACCION"];
            ViewBag.ENTIDAD_CRITICIDAD = diccionario["ENTIDAD_CRITICIDAD"];
            ViewBag.BITACORA_FILTROS_BOTON_FILTRAR = diccionario["BITACORA_FILTROS_BOTON_FILTRAR"];
            ViewBag.ENTIDAD_FECHAHORA = diccionario["ENTIDAD_FECHAHORA"];
            ViewBag.ENTIDAD_USUARIO = diccionario["ENTIDAD_USUARIO"];
            ViewBag.ENTIDAD_ACCION = diccionario["ENTIDAD_ACCION"];
            ViewBag.ENTIDAD_CRITICIDAD = diccionario["ENTIDAD_CRITICIDAD"];
            ViewBag.ENTIDAD_DETALLE = diccionario["ENTIDAD_DETALLE"];
            ViewBag.BITACORA_WARNING_SIN_FECHA_INICIO = diccionario["BITACORA_WARNING_SIN_FECHA_INICIO"];
            ViewBag.BITACORA_WARNING_FECHAS_MAL = diccionario["BITACORA_WARNING_FECHAS_MAL"];

            ViewBag.BOTON_EXPORTAR_XML = diccionario["BOTON_EXPORTAR_XML"];
            ViewBag.BOTON_BITACORA_HIST = diccionario["BOTON_BITACORA_HIST"];




        }
    }
}
