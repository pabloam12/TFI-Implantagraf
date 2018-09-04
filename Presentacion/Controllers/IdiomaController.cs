using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class IdiomaController : Controller
    {

        // GET: Idioma
        public ActionResult Index()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                var ln = new NegocioIdioma();

                return View(ln.Listar());
            }

            return RedirectToAction("Index", "Home");
        }



        // GET: Idioma/Crear
        public ActionResult Crear()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Crear(Idioma idioma)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
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
            if ((String)Session["PerfilUsuario"] == "WebMaster")
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
            if ((String)Session["PerfilUsuario"] == "WebMaster")
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

    }
}
