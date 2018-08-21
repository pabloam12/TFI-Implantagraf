using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class CategoriaController : Controller
    {
          
        // GET: Categoria
        public ActionResult Index()
        {
            var ln = new NegocioCategoria();

            return View(ln.Listar());
        }

       

        // GET: Categoria/Crear
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Categoria categoria)
        {
            try
            {
                var ln = new NegocioCategoria();
                ln.Agregar(categoria);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Categoria/Editar
        public ActionResult Editar(Categoria categoria)
        {
            try
            {
                var ln = new NegocioCategoria();
                ln.ActualizarPorId(categoria);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Borrar
        public ActionResult Borrar(int id)
        {
            try
            {
                var ln = new NegocioCategoria();
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
