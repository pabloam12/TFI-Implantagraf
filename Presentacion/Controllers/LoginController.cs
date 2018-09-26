using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;
using Seguridad;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {

            Session["ErrorLogin"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var ln = new NegocioCuenta();
            var seg = new Privacidad();
            var usrSesion = usuario;

            // Usuario con Sesión activa.
            if (ln.ValidarSesionActiva(usuario.Usr))
            {
                Session["ErrorLogin"] = "Usted ya tiene una Sesión Activa";
                return RedirectToAction("Login");
            }

            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            Session["Excepcion"] = null;

            // Usuario incorrecto, solo devuelvo el error al Login.
            if (ln.ValidarUsuario(usuario.Usr))
            {
                Session["ErrorLogin"] = "Usuario o contraseña inválidos";
                return RedirectToAction("Login");
            }

            // Valido que la cuenta no este bloqueada.
            if (ln.ValidarBloqueoCuenta(usuario.Usr))

            { return RedirectToAction("CuentaBloqueada"); }

            //Encripto la contraseña.
            usuario.Psw = seg.EncriptarPsw(usuario.Psw);

            // Valido que la contraseña sea correcta, en caso negativo incremento intentos fallidos.
            if (ln.ValidarUsuarioPsw(usuario.Usr, usuario.Psw))
            {
                Session["ErrorLogin"] = "Usuario o contraseña inválidos";

                //Sumo intento fallido.
                if (ln.SumarIntentoFallido(usuario.Usr) == 3)
                {
                    ln.BloquearCuentaUsuario(usuario.Usr); //Bloqueo cuenta de Usuario.

                    return RedirectToAction("CuentaBloqueada");
                }

                return RedirectToAction("Login");
            }

            usrSesion = ln.Autenticar(usuario);

            //Error en la base de datos.
            if (usrSesion.Nombre == null || usrSesion.PerfilUsr.Descripcion == null)

            {
                //TODO MENSAJE correcto
                Session["Excepcion"] = "[Error Nº 47] - Error de Base de Datos";
                return RedirectToAction("Index", "Excepciones");
            }


            //Usuario Logueado correctamente, se mapean las variables de Sesión.

            Session["IdUsuario"] = usrSesion.Id.ToString();
            Session["NombreUsuario"] = usrSesion.Nombre;
            Session["PerfilUsuario"] = usrSesion.PerfilUsr.Descripcion;
            Session["EmailUsuario"] = usrSesion.Email;

            Session["UsrLogin"] = usrSesion.Usr;

            //Activo la Sesión.
            ln.ActivarSesionCuentaUsuario(usrSesion.Usr);

            
            return RedirectToAction("Index", "Home");

        }

        public ActionResult CuentaBloqueada(Usuario usuario)
        {
            Session["ErrorLogin"] = "Cuenta bloqueada.";
            return RedirectToAction("Login");
        }
    }
}








