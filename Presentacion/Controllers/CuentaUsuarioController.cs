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

namespace Presentacion.Controllers
{
    public class CuentaController : Controller
    {
        // GET: /Cuenta/Index
        public ActionResult Index()
        {
            var ln = new NegocioCuenta();

            return View(ln.informacionCuenta(16));
        }

    } 
}