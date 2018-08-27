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
            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                var ln = new NegocioPerfilUsr();

                return View(ln.Listar());
            }

            return RedirectToAction("Home","Index");
        }

       

        // GET: PerfilUsr/Crear
        public ActionResult Crear()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                return View();
            }

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult Crear(PerfilUsr perfilUsr)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
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

            return RedirectToAction("Index","Home");
        }


        // GET: PerfilUsr/Editar
        public ActionResult Editar(PerfilUsr perfilUsr)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
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

            return RedirectToAction("Index","Home");
        }

        // GET: PerfilUsr/Borrar
        public ActionResult Borrar(int id)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
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

            return RedirectToAction("Index","Home");
        }
    }
}
