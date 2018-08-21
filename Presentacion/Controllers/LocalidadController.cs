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
            return View();
        }

       

        // GET: Localidad/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Localidad localidad)
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


        // GET: Localidad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: Localidad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

       
    }
}
