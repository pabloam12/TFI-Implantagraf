using Entidades;
using Negocio;
using Seguridad;
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
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == null && integ.ValidarExistencia("SEG_Usuario") == 1 && integ.ValidarExistencia("Idioma") == 1 && integ.ValidarExistencia("Localidad") == 1 && integ.ValidarExistencia("SEG_PerfilUsr") == 1 && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
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

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegistrarUsuarioWebMaster()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == null && integ.ValidarExistencia("SEG_Usuario") == 1 && integ.ValidarExistencia("Idioma") == 1 && integ.ValidarExistencia("Localidad") == 1 && integ.ValidarExistencia("SEG_PerfilUsr") == 1 && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
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

            usuario.Idioma = new Idioma { Id = 1, Descripcion = "Español", Abreviacion = "Esp" };
            usuario.PerfilUsr = new PerfilUsr { Id = 1, Descripcion = "WebMaster" };
            usuario.Localidad = new Localidad { Id = 1, Descripcion = "Implantagraf" };

            // Registro Usuario.
            var usrWebMaster = ln.RegistrarUsuario(usuario);

            ln.OtorgarPermisosWebmaster(usrWebMaster.Id);

            return RedirectToAction("Index");

        }


    }
}
