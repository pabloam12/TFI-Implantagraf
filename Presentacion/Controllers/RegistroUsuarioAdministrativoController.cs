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
    public class RegistroUsuarioAdministrativoController : Controller
    {

        public ActionResult Index()

        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Usuario") == 1 && integ.ValidarExistencia("Idioma") == 1 && integ.ValidarExistencia("Localidad") == 1 && integ.ValidarExistencia("SEG_PerfilUsr") == 1 && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
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

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegistrarUsuarioAdministrativo()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Usuario") == 1 && integ.ValidarExistencia("Idioma") == 1 && integ.ValidarExistencia("Localidad") == 1 && integ.ValidarExistencia("SEG_PerfilUsr") == 1 && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RegistrarUsuarioAdministrativo(FrmRegistroAdministrativo registroAdministrativo)
        {
            if (registroAdministrativo.Nombre == null && registroAdministrativo.Apellido == null && registroAdministrativo.CUIL == null && registroAdministrativo.Email == null && registroAdministrativo.Usr == null && registroAdministrativo.Psw == null && registroAdministrativo.Direccion == null && registroAdministrativo.Telefono == null)
            {
                registroAdministrativo.Nombre = registroAdministrativo.Nombre_Eng;
                registroAdministrativo.Apellido = registroAdministrativo.Apellido_Eng;
                registroAdministrativo.CUIL = registroAdministrativo.CUIL_Eng;
                registroAdministrativo.Email = registroAdministrativo.Email_Eng;
                registroAdministrativo.Usr = registroAdministrativo.Usr_Eng;
                registroAdministrativo.Psw = registroAdministrativo.Psw_Eng;
                registroAdministrativo.Direccion = registroAdministrativo.Direccion_Eng;
                registroAdministrativo.Telefono = registroAdministrativo.Telefono_Eng;

            }

            try
            {
                var ws = new WebService();

                Session["Excepcion"] = "";

                if (ws.ValidarCUIT(registroAdministrativo.CUIL) == false)
                {
                    if ((String)Session["IdiomaApp"] == "Esp" || (String)Session["IdiomaApp"] == null)
                    {
                        Session["ErrorRegistro"] = "EL CUIT ES INVÁLIDO";
                    }
                    else

                    { Session["ErrorRegistro"] = "INVALID CUIT NUMBER"; }

                    return RedirectToAction("RegistrarUsuarioAdministrativo");
                }

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
                usuario.RazonSocial = usuario.Nombre + "_" + usuario.Apellido;

                usuario.Idioma = new Idioma { Id = 1, Descripcion = "Español", Abreviacion = "Esp" };
                usuario.PerfilUsr = new PerfilUsr { Id = 2, Descripcion = "Administrativo" };
                usuario.Localidad = new Localidad { Id = 1, Descripcion = "Implantagraf" };

                // Registro Usuario.
                var usrRegistrado = ln.RegistrarUsuario(usuario);

                ln.OtorgarPermisosAdministrativo(usrRegistrado.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR REGISTRO", "ERROR LEVE", "Error al intentar registrar usuario Administrativo.");
                return RedirectToAction("Index", "Home");
            }
        }


    }
}
