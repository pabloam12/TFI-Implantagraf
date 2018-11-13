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
    public class VentasController : Controller
    {

        
        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "Administrativo" && integ.ValidarExistencia("Operacion") == 1)
            {
                var ln = new NegocioOperaciones();

                //Traducir Página VENTAS.
                TraducirPagina((String)Session["IdiomaApp"]);

                var consulta = ln.ListarOperacionesPorTipo("VE");

                Session["ConsultaVentas"] = consulta;

                return View(consulta);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string fecha, string fechaFin, string usr)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "Administrativo" && integ.ValidarExistencia("Operacion") == 1)
            {
                var ln = new NegocioOperaciones();

                //Traducir Página Ventas.
                TraducirPagina((String)Session["IdiomaApp"]);

                Session["ErrorFiltroVentas"] = null;

                if (fecha == "" && fechaFin != "")
                {
                    fechaFin = "";

                    Session["ErrorFiltroVentas"] = ViewBag.BITACORA_WARNING_SIN_FECHA_INICIO;
                }

                if (fecha != "" && fechaFin != "")
                {
                    DateTime fechaDate = DateTime.Parse(fecha);

                    DateTime fechaFinDate = DateTime.Parse(fechaFin);

                    if (fechaDate >= fechaFinDate)
                    {
                        fecha = "";
                        fechaFin = "";

                        Session["ErrorFiltroVentas"] = ViewBag.BITACORA_WARNING_FECHAS_MAL;

                    }

                }

                if (fecha == "" && usr == "")
                {
                    return View(ln.ListarOperacionesPorTipo("VE"));

                }

                var consulta = ln.ListarVentasPorFiltro(fecha, fechaFin);

                Session["ConsultaVentas"] = consulta;

                return View(consulta);
            }
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ExportarXML()
        {
            var exportador = new Exportador();

            List<Operacion> consultaVentas = (List<Operacion>)Session["ConsultaVentas"];

            exportador.ExportarVentasXML(consultaVentas);

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
            ViewBag.VENTAS_TITULO = diccionario["VENTAS_TITULO"];
            ViewBag.ENTIDAD_FECHA_INICIO = diccionario["ENTIDAD_FECHA_INICIO"];
            ViewBag.ENTIDAD_FECHA_FIN = diccionario["ENTIDAD_FECHA_FIN"];
            ViewBag.BITACORA_WARNING_SIN_FECHA_INICIO = diccionario["BITACORA_WARNING_SIN_FECHA_INICIO"];
            ViewBag.BITACORA_WARNING_FECHAS_MAL = diccionario["BITACORA_WARNING_FECHAS_MAL"];
            ViewBag.BOTON_EXPORTAR_XML = diccionario["BOTON_EXPORTAR_XML"];
            ViewBag.BITACORA_FILTROS_BOTON_FILTRAR = diccionario["BITACORA_FILTROS_BOTON_FILTRAR"];

            ViewBag.ENTIDAD_FECHAHORA = diccionario["ENTIDAD_FECHAHORA"];
            ViewBag.ENTIDAD_RAZON_SOCIAL = diccionario["ENTIDAD_RAZON_SOCIAL"];
            ViewBag.ENTIDAD_IMPORTE_TOTAL = diccionario["ENTIDAD_IMPORTE_TOTAL"];     


        }
    }
}
