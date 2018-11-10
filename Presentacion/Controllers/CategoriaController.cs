using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Seguridad;

namespace Presentacion.Controllers
{
    public class CategoriaController : Controller
    {

        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Categoria") == 1)
            {
                var ln = new NegocioCategoria();

                return View(ln.Listar());
            }
            return RedirectToAction("Index", "Home");
        }



        // GET: Categoria/Crear
        public ActionResult Crear()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Categoria") == 1)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Crear(Categoria categoria)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Categoria") == 1)
            {
                var ln = new NegocioCategoria();
                ln.Agregar(categoria, (String)Session["UsrLogin"]);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }


        // GET: Categoria/Editar
        public ActionResult Editar(Categoria categoria)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Categoria") == 1)
            {
                try
                {
                    var ln = new NegocioCategoria();
                    ln.ActualizarPorId(categoria,(String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
                }
                catch { return View(); }
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Categoria/Borrar
        public ActionResult Borrar(int id)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Categoria") == 1)
            {
                var ln = new NegocioCategoria();
                ln.BorrarPorId(ln.BuscarPorId(id), (String)Session["UsrLogin"]);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}

