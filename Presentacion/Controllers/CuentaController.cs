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

        public ActionResult ActualizarDatosCuenta(Usuario usuarioModif)
        {
            if ((String)Session["PerfilUsuario"] != null)
            {
                try
                {
                    var ln = new NegocioCuenta();
                    ln.ActualizarDatosCuenta(usuarioModif);

                    return RedirectToAction("Index");
                }
                catch
                {
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