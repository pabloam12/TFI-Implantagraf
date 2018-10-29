using Entidades;
using Negocio;
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
            return RedirectToAction("Registrarse");
        }

        public ActionResult Registrarse()
        {
            var lnloc = new NegocioLocalidad();

            ViewBag.Localidades = lnloc.Listar();

            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(FrmRegistroCliente registroCliente)
        {
            var ln = new NegocioCuenta();

            var mensajeria = new Mensajeria();
            
            Session["ErrorRegistro"] = null;
            Session["Excepcion"] = null;

            // Usuario existente, solo devuelvo el error.
            if (ln.ValidarUsuario(registroCliente.Email) == false)
            {
                Session["ErrorRegistro"] = "Usuario ya existente.";
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
            usuario.Idioma = new Idioma { Id = 1, Descripcion = "Español", Abreviacion="es" };
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

            // Envío correo de bienvenida.
            var cuerpoMsj = "Bienvenido a Implantagraf. Muchas gracias por confiar en nosotros, esperamos que encuentres lo que buscas y no dudes en consultarnos por lo que necesites.";
            var asuntoMsj = "Bienvenido!!";
            mensajeria.EnviarCorreo("implantagraf@gmail.com", usuario.Email, asuntoMsj, cuerpoMsj);


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
                Session["Excepcion"] = "[Error ] - Error de Registro de Usuario.";
                return RedirectToAction("Index", "Excepciones");
            }

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
