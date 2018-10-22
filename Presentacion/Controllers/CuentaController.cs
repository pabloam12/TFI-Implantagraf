using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Presentacion.Models;
using Negocio;
using Entidades;

namespace Presentacion.Controllers
{
    public class CuentaController : Controller
    {
        // GET: /Cuenta/Index
        public ActionResult Index()
        {

            return RedirectToAction("DetalleCuenta");

        }

        public ActionResult ListarUsuarios()
        {
            var ln = new NegocioCuenta();

            return View(ln.ListarUsuarios());

        }

        public ActionResult DetalleCuenta()
        {
            if ((String)Session["PerfilUsuario"] != null)
            {
                var ln = new NegocioCuenta();

                var idUsuario = (String)Session["IdUsuario"];

                return View(ln.InformacionCuenta(idUsuario));
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerPermisosUsuario(int id)
        {
            var ln = new NegocioCuenta();

            return View(ln.VerPermisosUsuario(id));


        }

        public ActionResult BloquearCuenta (int id)
        {
            var ln = new NegocioCuenta();

            ln.BloquearCuenta(id);

            return View();

        }

        public ActionResult DesbloquearCuenta(int id)
        {
            var ln = new NegocioCuenta();

            ln.DesbloquearCuenta(id);

            return View();

        }

        public ActionResult ActualizarDatosCuenta(Usuario usuarioModif)
        {
            if ((String)Session["PerfilUsuario"] != null)
            {
                try
                {
                    var ln = new NegocioCuenta();

                    var usrAnterior = ln.InformacionCuenta(usuarioModif.Id.ToString());

                    if (usuarioModif.Direccion == null)
                    { usuarioModif.Direccion = usrAnterior.Direccion; }


                    if (usuarioModif.Telefono == null)
                    { usuarioModif.Telefono = usrAnterior.Telefono; }


                    if (usuarioModif.Localidad.Id == 0)
                    { usuarioModif.Localidad.Id = usrAnterior.Localidad.Id; }

                    if (usuarioModif.Idioma.Id == 0)
                    { usuarioModif.Idioma.Id = usrAnterior.Idioma.Id; }

                    ln.ActualizarDatosCuenta(usuarioModif);

                    return RedirectToAction("Index");
                }
                catch
                {
                    var lnIdio = new NegocioIdioma();
                    var lnLoc = new NegocioLocalidad();

                    ViewBag.Localidades = lnLoc.Listar();
                    ViewBag.Idiomas = lnIdio.Listar();

                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RecuperarPsw()
        {
            return View();
        }

    }
}