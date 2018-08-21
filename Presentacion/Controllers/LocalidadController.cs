﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;

namespace Presentacion.Controllers
{
    public class LocalidadController : Controller
    {
          
        // GET: Localidad
        public ActionResult Index()
        {
            var ln = new NegocioLocalidad();

            return View(ln.Listar());
        }

       

        // GET: Localidad/Crear
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Localidad localidad)
        {
            try
            {
                var ln = new NegocioLocalidad();
                ln.Agregar(localidad);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Localidad/Editar
        public ActionResult Editar(Localidad localidad)
        {
            try
            {
                var ln = new NegocioLocalidad();
                ln.ActualizarPorId(localidad);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Localidad/Borrar
        public ActionResult Borrar(int id)
        {
            try
            {
                var ln = new NegocioLocalidad();
                ln.BorrarPorId(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
    }
}
