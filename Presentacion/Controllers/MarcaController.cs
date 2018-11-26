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
    public class MarcaController : Controller
    {

        // GET: Marca
        public ActionResult Index()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Marca") == 1)
            {
                var ln = new NegocioMarca();

                return View(ln.Listar());
            }

            return RedirectToAction("Index", "Home");
        }



        // GET: Marca/Crear
        public ActionResult Crear()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Marca") == 1)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Crear(Marca marca)
        {
            var integ = new IntegridadDatos();


            //try
            //{
                var ln = new NegocioMarca();
                ln.Agregar(marca, (String)Session["UsrLogin"]);

                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}


        }


        // GET: Marca/Editar
        public ActionResult Editar(Marca marca)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Marca") == 1)
            {
                try
                {
                    var ln = new NegocioMarca();
                    ln.ActualizarPorId(marca, (String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
            }
                catch
            {
                return View();
            }
        }
            return RedirectToAction("Index", "Home");
        }

        // GET: Marca/Borrar
        public ActionResult Borrar(int id)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("Marca") == 1)
            {
                //try
                //{
                    var ln = new NegocioMarca();
                    ln.BorrarPorId(ln.BuscarPorId(id), (String)Session["UsrLogin"]);

                    return RedirectToAction("Index");
                //}
                //catch
                //{
                //    return View();
                //}
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
