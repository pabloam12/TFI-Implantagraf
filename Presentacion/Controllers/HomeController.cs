using AccesoDatos;
using Negocio;
using Seguridad;
using Servicios;
using System;
using System.Collections;
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



            // Se comprueba la integridad de la base.
            if (integridad.ValidarIntegridadGlobal())
            {

                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR INTEGRIDAD", "GRAVE", "Se detectaron problemas de integridad en la base de datos.");


                TraducirPagina((String)Session["IdiomaApp"]);

                if ((String)Session["PerfilUsuario"] == "WebMaster")
                {
                    return RedirectToAction("Index", "RescateIntegridad");
                }

                Session["Excepcion"] = "ERROR DE INTEGRIDAD DE BASE DE DATOS";
                return RedirectToAction("Index", "Excepciones");
            }


            TraducirPagina((String)Session["IdiomaApp"]);


            Session["Excepcion"] = null;

            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                
                return RedirectToAction("Index", "RescateIntegridad");
            }

            
            return View();
        }


        public ActionResult Informacion()
        {
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();
        }

        public ActionResult Contacto()
        {
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();
        }


        private void TraducirPagina(string idioma)
        {
            var traductor = new Traductor();
            var integridad = new IntegridadDatos();
            var diccionario = new Hashtable();

            // Buscar Traducciones de Idioma.
            if (idioma == null)
            { idioma = "Esp"; }


            //Devuelve el Hastable con todas las traducciones.
            diccionario = traductor.Traducir(idioma);
                        
            //Traduce Vista HOME.
            ViewBag.HOME_LEYENDA_PRINCIPAL_1 = diccionario["HOME_LEYENDA_PRINCIPAL_1"];
            ViewBag.HOME_LEYENDA_PRINCIPAL_2 = diccionario["HOME_LEYENDA_PRINCIPAL_2"];
            ViewBag.BOTON_CATALOGO = diccionario["BOTON_CATALOGO"];
            ViewBag.HOME_TITULO_OFERTA_SEMANAL = diccionario["HOME_TITULO_OFERTA_SEMANAL"];
            ViewBag.HOME_LEYENDA_OFERTA_DIAS = diccionario["HOME_LEYENDA_OFERTA_DIAS"];
            ViewBag.HOME_LEYENDA_OFERTA_HORAS = diccionario["HOME_LEYENDA_OFERTA_HORAS"];
            ViewBag.HOME_LEYENDA_OFERTA_MINUTOS = diccionario["HOME_LEYENDA_OFERTA_MINUTOS"];
            ViewBag.HOME_LEYENDA_OFERTA_SEGUNDOS = diccionario["HOME_LEYENDA_OFERTA_SEGUNDOS"];
            ViewBag.PRODUCTO_IMPRESORA_AUTOMATICA = diccionario["PRODUCTO_IMPRESORA_AUTOMATICA"];
            ViewBag.BOTON_COMPRAR = diccionario["BOTON_COMPRAR"];
            ViewBag.HOME_TITULO_BENEFICIOS = diccionario["HOME_TITULO_BENEFICIOS"];
            ViewBag.HOME_SERVICIO_LUGAR_LEYENDA_1 = diccionario["HOME_SERVICIO_LUGAR_LEYENDA_1"];
            ViewBag.HOME_SERVICIO_LUGAR_LEYENDA_2 = diccionario["HOME_SERVICIO_LUGAR_LEYENDA_2"];
            ViewBag.HOME_SERVICIO_ENVIO_LEYENDA_1 = diccionario["HOME_SERVICIO_ENVIO_LEYENDA_1"];
            ViewBag.HOME_SERVICIO_ENVIO_LEYENDA_2 = diccionario["HOME_SERVICIO_ENVIO_LEYENDA_2"];
            ViewBag.HOME_SERVICIO_MATERIALES_LEYENDA_1 = diccionario["HOME_SERVICIO_MATERIALES_LEYENDA_1"];
            ViewBag.HOME_SERVICIO_MATERIALES_LEYENDA_2 = diccionario["HOME_SERVICIO_MATERIALES_LEYENDA_2"];
            ViewBag.HOME_SERVICIO_ECOLOGIA_LEYENDA_1 = diccionario["HOME_SERVICIO_ECOLOGIA_LEYENDA_1"];
            ViewBag.HOME_SERVICIO_ECOLOGIA_LEYENDA_2 = diccionario["HOME_SERVICIO_ECOLOGIA_LEYENDA_2"];
            ViewBag.HOME_SERVICIO_TECNICO_LEYENDA_1 = diccionario["HOME_SERVICIO_TECNICO_LEYENDA_1"];
            ViewBag.HOME_SERVICIO_TECNICO_LEYENDA_2 = diccionario["HOME_SERVICIO_TECNICO_LEYENDA_2"];
            ViewBag.HOME_SERVICIO_IMPORTADOS_LEYENDA_1 = diccionario["HOME_SERVICIO_IMPORTADOS_LEYENDA_1"];
            ViewBag.HOME_SERVICIO_IMPORTADOS_LEYENDA_2 = diccionario["HOME_SERVICIO_IMPORTADOS_LEYENDA_2"];
            ViewBag.HOME_TITULO_FOLLETO = diccionario["HOME_TITULO_FOLLETO"];
            ViewBag.HOME_LEYENDA_FOLLETO = diccionario["HOME_LEYENDA_FOLLETO"];
            ViewBag.BOTON_SUSCRIPCION = diccionario["BOTON_SUSCRIPCION"];


            ViewBag.HOME_CONTACTO_TITULO_MAPA = diccionario["HOME_CONTACTO_TITULO_MAPA"];
            ViewBag.HOME_CONTACTO_TITULO = diccionario["HOME_CONTACTO_TITULO"];
            ViewBag.HOME_CONTACTO_LEYENDA = diccionario["HOME_CONTACTO_LEYENDA"];
            ViewBag.HOME_CONTACTO_TITULO_HORARIOS = diccionario["HOME_CONTACTO_TITULO_HORARIOS"];
            ViewBag.HOME_CONTACTO_TITULO_HORARIOS_SEMANA = diccionario["HOME_CONTACTO_TITULO_HORARIOS_SEMANA"];
            ViewBag.HOME_CONTACTO_TITULO_HORARIOS_SABADO = diccionario["HOME_CONTACTO_TITULO_HORARIOS_SABADO"];
            ViewBag.HOME_CONTACTO_TITULO_HORARIOS_DOMINGOS_FERIADOS = diccionario["HOME_CONTACTO_TITULO_HORARIOS_DOMINGOS_FERIADOS"];
            ViewBag.HOME_CONTACTO_TITULO_HORARIOS_CERRADO = diccionario["HOME_CONTACTO_TITULO_HORARIOS_CERRADO"];
            ViewBag.HOME_CONTACTO_TITULO_2 = diccionario["HOME_CONTACTO_TITULO_2"];
            ViewBag.HOME_CONTACTO_FORMULARIO_LEYENDA = diccionario["HOME_CONTACTO_FORMULARIO_LEYENDA"];
            ViewBag.BOTON_ENVIAR = diccionario["BOTON_ENVIAR"];



        }



    }
}