using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class RegistroUsuarioWebMasterController : Controller
    {

        public ActionResult Index()

        {
            try
            {
                var ln = new NegocioCuenta();

                return View(ln.ListarUsuariosPorPerfil(1));
            }
            catch
            {
                Session["Excepcion"] = "Error al Listar los UsuariosWebMasters.";
                return RedirectToAction("Index", "Excepciones");
            }
        }

        public ActionResult RegistrarUsuarioWebMaster()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuarioWebMaster(FrmRegistroWebMaster registroWebMaster)
        {
            //try
            //{
            Session["Excepcion"] = "";

            var ln = new NegocioCuenta();

            var usuario = new Usuario();

            //Características de "WebMaster".
            usuario.Nombre = registroWebMaster.Nombre;
            usuario.Apellido = registroWebMaster.Apellido;
            usuario.Email = registroWebMaster.Email;
            usuario.Usr = registroWebMaster.Usr;
            usuario.Psw = registroWebMaster.Psw;
                        
            usuario.Estado = "S";
            usuario.FechaAlta = DateTime.Now;
            usuario.FechaBaja = new DateTime(2000, 01, 01);

            usuario.Direccion = "N/A";
            usuario.CUIL = "N/A";
            usuario.Telefono = "N/A";
            usuario.RazonSocial = "N/A";

            usuario.Idioma = new Idioma { Id = 1, Descripcion = "Español", Abreviacion = "es" };
            usuario.PerfilUsr = new PerfilUsr { Id = 1, Descripcion = "WebMaster" };
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
