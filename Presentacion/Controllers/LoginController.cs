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
        public ActionResult Login(FrmLogin login)
        {
            var ln = new NegocioCuenta();
            var seg = new Privacidad();
           

            // Usuario con Sesión activa.
            if (ln.ValidarSesionActiva(login.Usuario))
            {
                Session["ErrorLogin"] = "Usted ya tiene una Sesión Activa";
                return RedirectToAction("Login");
            }

            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;
            Session["CodCliente"] = null;
            Session["DireccionUsuario"] = null;
            Session["RazonSocialUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            Session["Excepcion"] = null;

            // Usuario incorrecto, solo devuelvo el error al Login.
            if (ln.ValidarUsuario(login.Usuario))
            {
                Session["ErrorLogin"] = "Usuario o contraseña inválidos";
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
                Session["ErrorLogin"] = "Usuario o contraseña inválidos";

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
            Session["CodCliente"] = usrSesion.Id;
            Session["DireccionUsuario"] = usrSesion.Direccion;

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
            Session["ErrorLogin"] = "Cuenta bloqueada.";
            return RedirectToAction("Login");
        }
    }
}








