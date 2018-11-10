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
    public class LocalidadController : Controller
    {
          
        // GET: Localidad
        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Localidad") == 1)
            {
                var ln = new NegocioLocalidad();

                return View(ln.Listar());
            }
            return RedirectToAction("Index", "Home");
        }

       

        // GET: Localidad/Crear
        public ActionResult Crear()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Localidad") == 1)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Crear(Localidad localidad)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Localidad") == 1)
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
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Localidad") == 1)
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
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Localidad") == 1)
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
