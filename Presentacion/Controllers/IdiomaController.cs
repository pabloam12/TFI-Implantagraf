using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using System.Threading;
using System.Globalization;
using Seguridad;

namespace Presentacion.Controllers
{
    public class IdiomaController : Controller
    {

        // GET: Idioma
        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Idioma") == 1)
            {
                var ln = new NegocioIdioma();

                return View(ln.Listar());
            }

            return RedirectToAction("Index", "Home");
        }



        // GET: Idioma/Crear
        public ActionResult Crear()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Idioma") == 1)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Crear(Idioma idioma)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Idioma") == 1)
            {
                var ln = new NegocioIdioma();
                ln.Agregar(idioma, (String)Session["UsrLogin"]);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index","Home");
        }


        // GET: Idioma/Editar
        public ActionResult Editar(Idioma idioma)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Idioma") == 1)
            {
                try
                {
                    var ln = new NegocioIdioma();
                    ln.ActualizarPorId(idioma, (String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Idioma/Borrar
        public ActionResult Borrar(int id)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Idioma") == 1)
            {
                try
                {
                    var ln = new NegocioIdioma();
                    ln.BorrarPorId(ln.BuscarPorId(id), (String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult TraducirIdioma (string idioma)
        {
            Session["IdiomaApp"] = idioma;

            return RedirectToAction("Index", "Home");

        }
    }
}
