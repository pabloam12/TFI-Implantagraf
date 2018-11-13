using Entidades;
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
    public class RegistroClienteController : Controller
    {
        // GET: Login
        public ActionResult Index()

        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == null && integ.ValidarExistencia("SEG_Usuario") == 1 && integ.ValidarExistencia("Idioma") == 1 && integ.ValidarExistencia("Localidad") == 1 && integ.ValidarExistencia("SEG_PerfilUsr") == 1 && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                TraducirPagina((String)Session["IdiomaApp"]);

                return RedirectToAction("Registrarse");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registrarse()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == null && integ.ValidarExistencia("SEG_Usuario") == 1 && integ.ValidarExistencia("Idioma") == 1 && integ.ValidarExistencia("Localidad") == 1 && integ.ValidarExistencia("SEG_PerfilUsr") == 1 && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                var lnloc = new NegocioLocalidad();

            TraducirPagina((String)Session["IdiomaApp"]);

            ViewBag.Localidades = lnloc.Listar();

            return View();

            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Registrarse(FrmRegistroCliente registroCliente)
        {
            if (registroCliente.CUIL == null && registroCliente.Direccion == null && registroCliente.Email == null && registroCliente.Psw == null && registroCliente.RazonSocial == null && registroCliente.Telefono == null)
            {
                registroCliente.CUIL = registroCliente.CUIL_Eng;
                registroCliente.Direccion = registroCliente.Direccion_Eng;
                registroCliente.Email = registroCliente.Email_Eng;
                registroCliente.Psw = registroCliente.Psw_Eng;
                registroCliente.RazonSocial = registroCliente.RazonSocial_Eng;
                registroCliente.Telefono = registroCliente.Telefono_Eng;

            }

            var ln = new NegocioCuenta();

            var ws = new WebService();

            var mensajeria = new Mensajeria();

            TraducirPagina((String)Session["IdiomaApp"]);

            Session["ErrorRegistro"] = null;
            Session["Excepcion"] = null;

            // Usuario existente, solo devuelvo el error.
            if (ln.ValidarUsuario(registroCliente.Email) == false)
            {
                Session["ErrorRegistro"] = "EL CORREO DE REGISTRO YA EXISTE";
                return RedirectToAction("Registrarse");
            }

            if (ws.ValidarCUIT(registroCliente.CUIL) == false)
            {
                Session["ErrorRegistro"] = "EL CUIT ES INVÁLIDO";
                return RedirectToAction("Registrarse");
            }

            var usuario = new Usuario();

            usuario.RazonSocial = registroCliente.RazonSocial;
            usuario.Email = registroCliente.Email;
            usuario.Psw = registroCliente.Psw;
            usuario.CUIL = registroCliente.CUIL.ToString();
            usuario.Direccion = registroCliente.Direccion;
            usuario.Localidad = registroCliente.Localidad;
            usuario.PerfilUsr = new PerfilUsr { Id = 3, Descripcion = "Cliente" };
            usuario.Idioma = new Idioma { Id = 1, Descripcion = "Español", Abreviacion = "Esp" };
            usuario.Estado = "S";
            usuario.FechaAlta = DateTime.Now;
            usuario.FechaBaja = new DateTime(2000, 01, 01);

            // Características propias de Clientes.
            usuario.Nombre = registroCliente.RazonSocial;
            usuario.Apellido = registroCliente.RazonSocial;
            usuario.Usr = registroCliente.Email;
            usuario.Telefono = registroCliente.Telefono;

            // Registro Usuario.
            var usrSesion = ln.RegistrarUsuario(usuario);

            ln.OtorgarPermisosCliente(usrSesion.Id);

            try
            {
                // Envío correo de bienvenida.
                var cuerpoMsj = "Bienvenido a Implantagraf. Muchas gracias por confiar en nosotros, esperamos que encuentres lo que buscas y no dudes en consultarnos por lo que necesites.";
                var asuntoMsj = "Bienvenido!!";
                mensajeria.EnviarCorreo("implantagraf@gmail.com", usuario.Email, asuntoMsj, cuerpoMsj);
            }
            catch
            { //TODO mensaje bitacora
            }

            if (usrSesion.Nombre != "" && usrSesion.PerfilUsr.Descripcion != "")
            {
                Session["IdUsuario"] = usrSesion.Id.ToString();
                Session["NombreUsuario"] = usrSesion.Nombre;
                Session["RazonSocialUsuario"] = usrSesion.RazonSocial;
                Session["PerfilUsuario"] = usrSesion.PerfilUsr.Descripcion;
                Session["EmailUsuario"] = usrSesion.Email;
                Session["CodUsuario"] = usrSesion.Id;
                Session["DireccionUsuario"] = usrSesion.Direccion;

                Session["UsrLogin"] = usrSesion.Usr;

                HttpCookie cookie = new HttpCookie("UsrLogin");
                cookie.Value = usrSesion.Usr;
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //TODO.
                Session["Excepcion"] = ViewBag.ERROR_REGISTRO_USUARIO;
                return RedirectToAction("Index", "Excepciones");
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

            //Traduce Vistas REGISTRO USUARIO.
            ViewBag.REGISTRO_CLIENTE_TITULO = diccionario["REGISTRO_CLIENTE_TITULO"];
            ViewBag.ENTIDAD_RAZON_SOCIAL = diccionario["ENTIDAD_RAZON_SOCIAL"];
            ViewBag.ENTIDAD_USUARIO_MAIL = diccionario["ENTIDAD_USUARIO_MAIL"];
            ViewBag.ENTIDAD_CUIL = diccionario["ENTIDAD_CUIL"];
            ViewBag.ENTIDAD_TELEFONO = diccionario["ENTIDAD_TELEFONO"];
            ViewBag.ENTIDAD_DIRECCION = diccionario["ENTIDAD_DIRECCION"];
            ViewBag.ENTIDAD_LOCALIDAD = diccionario["ENTIDAD_LOCALIDAD"];
            ViewBag.ENTIDAD_PSW = diccionario["ENTIDAD_PSW"];
            ViewBag.ENTIDAD_CONFIRMACION_PSW = diccionario["ENTIDAD_CONFIRMACION_PSW"];
            ViewBag.BOTON_REGISTRAR = diccionario["BOTON_REGISTRAR"];
            ViewBag.ERROR_CUIT = diccionario["ERROR_CUIT"];
        }
    }
}
