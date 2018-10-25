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
    public class BitacoraController : Controller
    {

        // GET: Idioma
        public ActionResult Index()
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")
            {
                var ln = new Auditoria();


                return View(ln.ConsultarBitacora());
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string fecha, string fechaFin, string usr, string accion, string criticidad)
        {
            if ((String)Session["PerfilUsuario"] == "WebMaster")

            {
                var ln = new Auditoria();

                if (fecha == "" && fechaFin != "")
                {
                    fechaFin = "";

                    Session["ErrorFiltroBitacora"] = Recursos.Recursos.BITACORA_WARNING_SIN_FECHA_INICIO;
                }

                if (fecha != "" && fechaFin != "")
                {
                    DateTime fechaDate = DateTime.Parse(fecha);

                    DateTime fechaFinDate = DateTime.Parse(fechaFin);

                    if (fechaDate >= fechaFinDate)
                    {
                        fecha = "";
                        fechaFin = "";

                        Session["ErrorFiltroBitacora"] = Recursos.Recursos.BITACORA_WARNING_FECHAS_MAL;

                    }

                }

                if (fecha == "" && usr == "" && accion == "" && criticidad == "")
                {
                    return View(ln.ConsultarBitacora());

                }

                // Si la fecha de fin es posterior a la de inicio no la tiene en cuenta.
                //if (DateTime.Compare(Convert.ToDateTime(fecha).Date , Convert.ToDateTime(fechaFin).Date) >= 0)

                //{ fechaFin = ""; }

                return View(ln.ConsultarBitacora(fecha, fechaFin, usr, accion, criticidad));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
