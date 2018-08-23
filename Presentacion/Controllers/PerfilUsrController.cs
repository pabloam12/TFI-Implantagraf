using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class PerfilUsrController : Controller
    {
          
        // GET: PerfilUsr
        public ActionResult Index()
        {
            var ln = new NegocioPerfilUsr();

            return View(ln.Listar());
        }

       

        // GET: PerfilUsr/Crear
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(PerfilUsr perfilUsr)
        {
            try
            {
                var ln = new NegocioPerfilUsr();
                ln.Agregar(perfilUsr);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: PerfilUsr/Editar
        public ActionResult Editar(PerfilUsr perfilUsr)
        {
            try
            {
                var ln = new NegocioPerfilUsr();
                ln.ActualizarPorId(perfilUsr);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PerfilUsr/Borrar
        public ActionResult Borrar(int id)
        {
            try
            {
                var ln = new NegocioPerfilUsr();
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
