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
    public class PerfilUsrController : Controller
    {
          
        // GET: PerfilUsr
        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_PerfilUsr") == 1)
            {
                var ln = new NegocioPerfilUsr();

                return View(ln.Listar());
            }

            return RedirectToAction("Home","Index");
        }

       

        // GET: PerfilUsr/Crear
        public ActionResult Crear()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_PerfilUsr") == 1)
            {
                return View();
            }

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult Crear(PerfilUsr perfilUsr)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_PerfilUsr") == 1)
            {
                try
                {
                    var ln = new NegocioPerfilUsr();
                    ln.Agregar(perfilUsr, (String)Session["UsrLogin"]);

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
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_PerfilUsr") == 1)
            {
                try
                {
                    var ln = new NegocioPerfilUsr();
                    ln.ActualizarPorId(perfilUsr, (String)Session["UsrLogin"]);

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
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_PerfilUsr") == 1)
            {
                try
                {
                    var ln = new NegocioPerfilUsr();
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
