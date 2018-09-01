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
            var ln = new NegocioCuenta();

            return View(ln.ListarUsuariosPorPerfil(1));
        }

        public ActionResult RegistrarUsuarioWebMaster()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuarioWebMaster(Usuario usuario)
        {
            var ln = new NegocioCuenta();

            var perfil = 1;
            var localidad = 1;
            var idioma = 1;

            usuario.Direccion = "N/A";
            usuario.CUIL = "N/A";
            usuario.Direccion = "N/A";
            usuario.Telefono = "N/A";
            usuario.FechaNacimiento = DateTime.Now;


            ln.RegistrarUsuario(usuario, perfil, idioma, localidad);


            return RedirectToAction("Index");
        }

        //public ActionResult Borrar(int id)
        //{
        //    if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "AdministradorWeb")
        //    {
        //        var ln = new NegocioCuenta();
        //        ln.BorrarPorId(id);

        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

    }
}
