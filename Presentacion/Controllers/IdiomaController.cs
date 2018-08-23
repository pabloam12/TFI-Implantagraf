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
            var ln = new NegocioIdioma();

            return View(ln.Listar());
        }



        // GET: Idioma/Crear
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Idioma idioma)
        {
            try
            {
                var ln = new NegocioIdioma();
                ln.Agregar(idioma);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Idioma/Editar
        public ActionResult Editar(Idioma idioma)
        {
            try
            {
                var ln = new NegocioIdioma();
                ln.ActualizarPorId(idioma);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Idioma/Borrar
        public ActionResult Borrar(int id)
        {
            try
            {
                var ln = new NegocioIdioma();
                ln.BorrarPorId(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
