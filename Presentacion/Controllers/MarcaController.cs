﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class MarcaController : Controller
    {
          
        // GET: Marca
        public ActionResult Index()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "AdministradorWeb")
            {
                var ln = new NegocioMarca();

                return View(ln.Listar());
            }

            return RedirectToAction("Index","Home");
        }

       

        // GET: Marca/Crear
        public ActionResult Crear()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "AdministradorWeb")
            {
                return View();
            }

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult Crear(Marca marca)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "AdministradorWeb")
            {
                try
                {
                    var ln = new NegocioMarca();
                    ln.Agregar(marca);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return RedirectToAction("Index","Home");
        }


        // GET: Marca/Editar
        public ActionResult Editar(Marca marca)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "AdministradorWeb")
            {
                try
                {
                    var ln = new NegocioMarca();
                    ln.ActualizarPorId(marca);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Index","Home");
        }

        // GET: Marca/Borrar
        public ActionResult Borrar(int id)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster" || (String)Session["PerfilUsuario"] == "AdministradorWeb")
            {
                try
                {
                    var ln = new NegocioMarca();
                    ln.BorrarPorId(id);

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
