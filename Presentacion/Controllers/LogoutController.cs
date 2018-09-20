﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Seguridad;
using Negocio;

namespace Presentacion.Controllers
{
    public class LogoutController : Controller
    {

        public ActionResult Index()
        {
            var aud = new Auditoria();
            var inte = new IntegridadDatos();

            var ln = new NegocioCuenta();

            var BitacoraDVH = inte.CalcularDVH((String)Session["UsrLogin"] + "LOGIN DE USUARIO" + "INFO");

            aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "CIERRE DE SESIÓN", "INFO", "El Usuario ha cerrado sesión.", BitacoraDVH);

            ln.ActivarCuentaUsuario((String)Session["UsrLogin"]);

            Session["ErrorLogin"] = null;
            Session["IdUsuario"] = null;
            Session["NombreUsuario"] = null;
            Session["PerfilUsuario"] = null;
            Session["EmailUsuario"] = null;

            Session["UsrLogin"] = null;

            Session["ErrorLogin"] = null;
            Session["Excepcion"] = null;
     
            return RedirectToAction("Index", "Home");
        }

    }
}




