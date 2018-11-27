using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Seguridad;
using Servicios;
using Entidades;

namespace Presentacion.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "Administrativo" && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                try
                {
                    var ln = new NegocioCuenta();

                    //Traducir Página CLientes.
                    TraducirPagina((String)Session["IdiomaApp"]);

                    var consulta = ln.ListarUsuariosPorPerfil(3);

                    Session["ConsultaBitacora"] = consulta;

                    return View(consulta);
                }

                catch
                {
                    var aud = new Auditoria();
                    aud.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR CLIENTES", "ERROR LEVE", "Error al consultar clientes.");
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string fecha, string fechaFin, string usr)
        {

            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "Administrativo" && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                try
                {

                    var ln = new NegocioCuenta();

                    //Traducir Página CLIENTE.
                    TraducirPagina((String)Session["IdiomaApp"]);

                    Session["ErrorFiltroCliente"] = null;

                    if (fecha == "" && fechaFin != "")
                    {
                        fechaFin = "";

                        Session["ErrorFiltroCliente"] = ViewBag.BITACORA_WARNING_SIN_FECHA_INICIO;
                    }

                    if (fecha != "" && fechaFin != "")
                    {
                        DateTime fechaDate = DateTime.Parse(fecha);

                        DateTime fechaFinDate = DateTime.Parse(fechaFin);

                        if (fechaDate >= fechaFinDate)
                        {
                            fecha = "";
                            fechaFin = "";

                            Session["ErrorFiltroCliente"] = ViewBag.BITACORA_WARNING_FECHAS_MAL;

                        }

                    }

                    if (fecha == "" && usr == "")
                    {
                        return View(ln.ListarUsuariosPorPerfil(3));

                    }

                    var consulta = ln.ListarClientesPorFiltro(fecha, fechaFin, usr);

                    Session["ConsultaCliente"] = consulta;

                    return View(consulta);

                }
                catch
                {
                    var aud = new Auditoria();
                    aud.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR CLIENTES", "ERROR LEVE", "Error al consultar los clientes.");
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");


        }


        public ActionResult ExportarXML()
        {
            try { 
            var exportador = new Exportador();

            List<Usuario> consultaBitacora = (List<Usuario>)Session["ConsultaCliente"];

            exportador.ExportarClientesXML(consultaBitacora);

                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "EXPORTA XML", "INFO", "El usuario exporto un listado XML.");


                return RedirectToAction("Index");
            }
            catch
            {
                Session["Excepcion"] = "ERROR AL EXPORTAR XML CLIENTES";

                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "EXPORTA XML", "LEVE", "Error al exportar listado XML.");


                return RedirectToAction("Index", "Home");
            }
        }

        private void TraducirPagina(string idioma)
        {
            var traductor = new Traductor();

            // Buscar Traducciones de Idioma.
            if (idioma == null)
            { idioma = "Esp"; }

            //Devuelve el Hastable con todas las traducciones.
            var diccionario = traductor.Traducir(idioma);

            //Traduce Vista CLIENTE.
            ViewBag.BITACORA_TITULO = diccionario["BITACORA_TITULO"];
            ViewBag.ENTIDAD_FECHA_INICIO = diccionario["ENTIDAD_FECHA_INICIO"];
            ViewBag.ENTIDAD_FECHA_FIN = diccionario["ENTIDAD_FECHA_FIN"];
            ViewBag.ENTIDAD_USUARIO = diccionario["ENTIDAD_USUARIO"];
            ViewBag.BITACORA_FILTROS_BOTON_FILTRAR = diccionario["BITACORA_FILTROS_BOTON_FILTRAR"];
            ViewBag.ENTIDAD_FECHA_ALTA = diccionario["ENTIDAD_FECHA_ALTA"];
            ViewBag.ENTIDAD_USUARIO = diccionario["ENTIDAD_USUARIO"];
            ViewBag.ENTIDAD_RAZON_SOCIAL = diccionario["ENTIDAD_RAZON_SOCIAL"];
            ViewBag.ENTIDAD_CUIL = diccionario["ENTIDAD_CUIL"];
            ViewBag.ENTIDAD_DIRECCION = diccionario["ENTIDAD_DIRECCION"];
            ViewBag.BITACORA_WARNING_SIN_FECHA_INICIO = diccionario["BITACORA_WARNING_SIN_FECHA_INICIO"];
            ViewBag.BITACORA_WARNING_FECHAS_MAL = diccionario["BITACORA_WARNING_FECHAS_MAL"];

            ViewBag.BOTON_EXPORTAR_XML = diccionario["BOTON_EXPORTAR_XML"];

            ViewBag.CLIENTE_TITULO = diccionario["CLIENTE_TITULO"];


        }
    }
}