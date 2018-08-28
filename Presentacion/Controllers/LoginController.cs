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
            
            Session["ErrorLogin"] = null;
            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;
            Session["UsrLogin"] = null;

            // Usuario incorrecto, solo devuelvo el error al Login.
            if (ln.ValidarUsuario(usuario.Usr))
            {
                Session["ErrorLogin"] = "Usuario o contraseña inválidos";
                return RedirectToAction("Index");
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

                return RedirectToAction("Index");
            }

            usrSesion = ln.Autenticar(usuario);

            //Error en la base de datos.
            if (usrSesion.Nombre == null || usrSesion.Perfil.Descripcion == null)
                //TODO.
            { return RedirectToAction("ErrorGraveBase"); }


            //Usuario Logueado correctamente, se mapean las variables de Sesión.

            Session["IdUsuario"] = usrSesion.Id.ToString();
            Session["UsrLogin"] = usrSesion.Usr;
            Session["NombreUsuario"] = usrSesion.Nombre;
            Session["PerfilUsuario"] = usrSesion.Perfil.Descripcion;
            Session["EmailUsuario"] = usrSesion.Email;

            return RedirectToAction("Index", "Home");

        }
        public ActionResult CuentaBloqueada(Usuario usuario)
        {
            //TODO.
            Session["ErrorLogin"] = "Cuenta bloqueada.";
            return RedirectToAction("Index");
        }
    }
}








