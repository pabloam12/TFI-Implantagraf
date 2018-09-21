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
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "Administrativo")
            {
                var ln = new NegocioLocalidad();

                return View(ln.Listar());
            }
            return RedirectToAction("Index", "Home");
        }

       

        // GET: Localidad/Crear
        public ActionResult Crear()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "Administrativo")
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Crear(Localidad localidad)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "Administrativo")
            {
                try
                {
                    var ln = new NegocioLocalidad();
                    ln.Agregar(localidad, (String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return RedirectToAction("Index","Home");
        }


        // GET: Localidad/Editar
        public ActionResult Editar(Localidad localidad)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "Administrativo")
            {
                try
                {
                    var ln = new NegocioLocalidad();
                    ln.ActualizarPorId(localidad, (String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return RedirectToAction("Index","Home");

        }

        // GET: Localidad/Borrar
        public ActionResult Borrar(int id)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "Administrativo")
            {
                try
                {
                    var ln = new NegocioLocalidad();

                    var localidad = ln.BuscarPorId(id);

                    ln.BorrarPorId(localidad, (String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }

       
    }
}
