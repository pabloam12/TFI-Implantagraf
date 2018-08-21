using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class LocalidadController : Controller
    {
          
        // GET: Localidad
        public ActionResult Index()
        {
            var ln = new NegocioLocalidad();

            return View(ln.Listar());
        }

       

        // GET: Localidad/Create
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Localidad localidad)
        {
            try
            {
                var ln = new NegocioLocalidad();
                ln.Agregar(localidad);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Localidad/Edit
        public ActionResult Editar(int id)
        {
            return View();
        }

        // GET: Localidad/Delete/5
        public ActionResult Borrar(int id)
        {
            return View();
        }

       
    }
}
