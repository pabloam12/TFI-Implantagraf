using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Seguridad;
using Servicios;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == null && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                Session["ErrorLogin"] = null;

                // Traduce páginas de LOGIN.
                TraducirPagina((String)Session["IdiomaApp"]);

                return RedirectToAction("Login");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            // Traduce páginas de LOGIN.
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();
        }


        [HttpPost]
        public ActionResult Login(FrmLogin login)
        {
            if (login.Usuario == null && login.Contraseña == null)
            {
                login.Usuario = login.Usuario_Eng;
                login.Contraseña = login.Contraseña_Eng;
            }

            var ln = new NegocioCuenta();
            var seg = new Privacidad();

            // Traduce páginas de LOGIN.
            TraducirPagina((String)Session["IdiomaApp"]);

            // Usuario con Sesión activa.
            if (ln.ValidarSesionActiva(login.Usuario))
            {
                Session["ErrorLogin"] = ViewBag.ERROR_LOGIN_SESION_ACTIVA;
                return RedirectToAction("Login");
            }

            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;
            Session["CodUsuario"] = null;
            Session["DireccionUsuario"] = null;
            Session["RazonSocialUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            Session["Excepcion"] = null;

            // Usuario incorrecto, solo devuelvo el error al Login.
            if (ln.ValidarUsuario(login.Usuario))
            {
                Session["ErrorLogin"] = ViewBag.ERROR_LOGIN_USUARIO_PSW_INVALIDOS;
                return RedirectToAction("Login");
            }

            // Valido que la cuenta no este bloqueada.
            if (ln.ValidarBloqueoCuenta(login.Usuario))

            { return RedirectToAction("CuentaBloqueada"); }

            //Encripto la contraseña.
            login.Contraseña = seg.EncriptarPsw(login.Contraseña);

            // Valido que la contraseña sea correcta, en caso negativo incremento intentos fallidos.
            if (ln.ValidarUsuarioPsw(login.Usuario, login.Contraseña))
            {
                Session["ErrorLogin"] = ViewBag.ERROR_LOGIN_USUARIO_PSW_INVALIDOS;

                //Sumo intento fallido.
                if (ln.SumarIntentoFallido(login.Usuario) == 3)
                {
                    ln.BloquearCuentaUsuario(login.Usuario); //Bloqueo cuenta de Usuario.

                    return RedirectToAction("CuentaBloqueada");
                }

                return RedirectToAction("Login");
            }

            var usrSesion = ln.Autenticar(login);

            //Error en la base de datos.
            if (usrSesion.Nombre == null || usrSesion.PerfilUsr.Descripcion == null)

            {
                //TODO MENSAJE correcto
                Session["Excepcion"] = "[Error Nº 0] - Error de Base de Datos";
                return RedirectToAction("Index", "Excepciones");
            }

            //Usuario Logueado correctamente, se mapean las variables de Sesión.

            Session["IdUsuario"] = usrSesion.Id.ToString();
            Session["NombreUsuario"] = usrSesion.Nombre;
            Session["RazonSocialUsuario"] = usrSesion.RazonSocial;
            Session["PerfilUsuario"] = usrSesion.PerfilUsr.Descripcion;
            Session["EmailUsuario"] = usrSesion.Email;
            Session["CodUsuario"] = usrSesion.Id;
            Session["DireccionUsuario"] = usrSesion.Direccion;
            Session["IdiomaApp"] = usrSesion.Idioma.Abreviacion;

            Session["UsrLogin"] = usrSesion.Usr;

            HttpCookie cookie = new HttpCookie("UsrLogin");
            cookie.Value = usrSesion.Usr;
            Response.Cookies.Add(cookie);

            //Activo la Sesión.
            //ln.ActivarSesionCuentaUsuario(usrSesion.Usr);

            return RedirectToAction("Index", "Home");

        }

        public ActionResult CuentaBloqueada(Usuario usuario)
        {
            // Traduce páginas de LOGIN.
            TraducirPagina((String)Session["IdiomaApp"]);

            Session["ErrorLogin"] = ViewBag.ERROR_LOGIN_CUENTA_BLOQUEADA;
            return RedirectToAction("Login");
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
            ViewBag.LOGIN_TITULO_TENGO_CUENTA = diccionario["LOGIN_TITULO_TENGO_CUENTA"];
            ViewBag.ENTIDAD_USUARIO = diccionario["ENTIDAD_USUARIO"];
            ViewBag.ENTIDAD_PSW = diccionario["ENTIDAD_PSW"];
            ViewBag.LOGIN_OLVIDO_PSW = diccionario["LOGIN_OLVIDO_PSW"];
            ViewBag.LOGIN_TITULO_USUARIO_NUEVO = diccionario["LOGIN_TITULO_USUARIO_NUEVO"];
            ViewBag.BOTON_REGISTRAR = diccionario["BOTON_REGISTRAR"];

            ViewBag.ERROR_LOGIN_SESION_ACTIVA = diccionario["ERROR_LOGIN_SESION_ACTIVA"];
            ViewBag.ERROR_LOGIN_USUARIO_PSW_INVALIDOS = diccionario["ERROR_LOGIN_USUARIO_PSW_INVALIDOS"];
            ViewBag.ERROR_LOGIN_CUENTA_BLOQUEADA = diccionario["ERROR_LOGIN_CUENTA_BLOQUEADA"];

        }
    }
}








