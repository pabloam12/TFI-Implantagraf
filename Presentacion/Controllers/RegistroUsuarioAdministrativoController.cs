using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class RegistroUsuarioAdministrativoController : Controller
    {

        public ActionResult Index()

        {
            try
            {
                var ln = new NegocioCuenta();

                return View(ln.ListarUsuariosPorPerfil(2));
            }
            catch
            {
                Session["Excepcion"] = "Error al Listar los UsuariosAdministrativos.";
                return RedirectToAction("Index", "Excepciones");
            }
        }

        public ActionResult RegistrarUsuarioAdministrativo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuarioAdministrativo(FrmRegistroAdministrativo registroAdministrativo)
        {
            //try
            //{
            Session["Excepcion"] = "";

            var ln = new NegocioCuenta();

            var usuario = new Usuario();

            //Características de "Administrativo".
            usuario.Nombre = registroAdministrativo.Nombre;
            usuario.Apellido = registroAdministrativo.Apellido;
            usuario.Email = registroAdministrativo.Email;
            usuario.Usr = registroAdministrativo.Usr;
            usuario.Psw = registroAdministrativo.Psw;
                        
            usuario.Estado = "S";
            usuario.FechaAlta = DateTime.Now;
            usuario.FechaBaja = new DateTime(2000, 01, 01);

            usuario.Direccion = registroAdministrativo.Direccion;
            usuario.CUIL = registroAdministrativo.CUIL;
            usuario.Telefono = registroAdministrativo.Telefono;
            usuario.RazonSocial = "N/A";

            usuario.Idioma = new Idioma { Id = 1, Descripcion = "Español", Abreviacion = "Esp" };
            usuario.PerfilUsr = new PerfilUsr { Id = 2, Descripcion = "Administrativo" };
            usuario.Localidad = new Localidad { Id = 1, Descripcion = "Implantagraf" };
                        
            // Registro Usuario.
            ln.RegistrarUsuario(usuario);

            return RedirectToAction("Index");
            //}
            //catch
            //{
            //TODO
            //    Session["Excepcion"] = "Error al Registrar Usuario.";
            //    return RedirectToAction("Index", "Excepciones");
            //}
        }


    }
}
