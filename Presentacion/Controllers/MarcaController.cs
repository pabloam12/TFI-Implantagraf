using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class MarcaController : Controller
    {
          
        // GET: Marca
        public ActionResult Index()
        {
            var ln = new NegocioMarca();

            return View(ln.Listar());
        }

       

        // GET: Marca/Crear
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Marca marca)
        {
            try
            {
                var ln = new NegocioMarca();
                ln.Agregar(marca);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Marca/Editar
        public ActionResult Editar(Marca marca)
        {
            try
            {
                var ln = new NegocioMarca();
                ln.ActualizarPorId(marca);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Marca/Borrar
        public ActionResult Borrar(int id)
        {
            try
            {
                var ln = new NegocioMarca();
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
