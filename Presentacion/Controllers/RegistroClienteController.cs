using Entidades;
using Negocio;
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

            // Características propias de Clientes.
            usuario.Nombre = registroCliente.RazonSocial;
            usuario.Apellido = "N/A";
            usuario.Usr = registroCliente.Email;
            usuario.Telefono = "N/A";

            // Registro Usuario.
            var usrSesion = ln.RegistrarUsuario(usuario);

            if (usrSesion.Nombre != "" && usrSesion.PerfilUsr.Descripcion != "")
            {
                Session["IdUsuario"] = usrSesion.Id.ToString();
                Session["NombreUsuario"] = usrSesion.Nombre;
                Session["PerfilUsuario"] = usrSesion.PerfilUsr.Descripcion;
                Session["EmailUsuario"] = usrSesion.Email;

                Session["UsrLogin"] = usrSesion.Usr;

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
