using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Negocio;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        //public ActionResult Registrar()

        //{
        //    return View();
        //}

        //[HttpPost]

        //public ActionResult Registrar(Usuario usuario)

        //{
        //    var ln = new NegocioCuenta();

        //    ln.Registrar(usuario);

        //    ViewBag.Message = usuario.Nombre + " " + usuario.Apellido + " se ha registrado correctamente.";

        //    return View();
        //    //db.cuentaUsuario.Add(cuenta);
        //    //db.SaveChanges();
        //}


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {

            var ln = new NegocioCuenta();

            var usrSesion = ln.Autenticar(usuario);

            if (usrSesion != null)
            {

                Session["UsuarioNombre"] = usrSesion.Nombre.ToString();

                //ViewBag.Message = usr.Nombre + " " + usr.Apellido + " se ha registrado correctamente.";

                return View(); // LoggedIn
            }
            else
            {
                Session["UsuarioNombre"] = null;
                return RedirectToAction("Login");

            }
        }
        //using (CuentaDBContext db = new CuentaDBContext())
        //{
        //    var usr = db.cuentaUsuario.Single(u => u.Usr == usuario.Usr && u.Psw == usuario.Psw);

        //    if (usr != null)
        //    {
        //        Session["UsuarioID"] = usr.ID.ToString();
        //        Session["UsuarioNombre"] = usr.Nombre.ToString();

        //        return RedirectToAction("Logeado"); // LoggedIn
        //    }
        //    else
        //    {

        //        return RedirectToAction("Login");
        //        //ModelState.AddModelError("Nombre de Usuario o Contraseña Erroneos");

        //    }

        //}

    }

    //public ActionResult Logeado()
    //{
    //    if (Session["UsuarioID"] != null)
    //    {
    //        return View();
    //    }
    //    else
    //    {

    //        return RedirectToAction("Login");
    //    }

    //}



}




