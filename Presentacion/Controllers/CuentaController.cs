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
using Servicios;
using Seguridad;

namespace Presentacion.Controllers
{
    public class CuentaController : Controller
    {
        // GET: /Cuenta/Index
        public ActionResult Index()
        {
            // Traduce páginas de CUENTA.
            TraducirPagina((String)Session["IdiomaApp"]);

            return RedirectToAction("DetalleCuenta");

        }

        public ActionResult ListarUsuarios()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                var ln = new NegocioCuenta();

                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                return View(ln.ListarUsuarios());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetalleCuenta()
        {
            if ((String)Session["PerfilUsuario"] != null)
            {
                var ln = new NegocioCuenta();

                var idUsuario = (String)Session["IdUsuario"];

                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                return View(ln.InformacionCuenta(idUsuario));
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerPermisosUsuario(int idUsr)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                var ln = new NegocioCuenta();

                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                return View(ln.VerDetallePermisosUsuario(idUsr));
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult SacarPermiso(int idDetallePermiso)
        {
            var ln = new NegocioCuenta();

            var detalleModificado = ln.SacarPermiso(idDetallePermiso);

            return RedirectToAction("VerPermisosUsuario", new { idUsr = detalleModificado.UsrId });
        }

        public ActionResult DarPermiso(int idDetallePermiso)
        {
            var ln = new NegocioCuenta();

            var detalleModificado = ln.DarPermiso(idDetallePermiso);

            return RedirectToAction("VerPermisosUsuario", new { idUsr = detalleModificado.UsrId });
        }



        public ActionResult BloquearCuenta(int id)
        {
            var ln = new NegocioCuenta();

            ln.BloquearCuenta(id);

            // Traduce páginas de CUENTA.
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();

        }

        public ActionResult DesbloquearCuenta(int id)
        {
            var ln = new NegocioCuenta();

            ln.DesbloquearCuenta(id);

            // Traduce páginas de CUENTA.
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();

        }

        public ActionResult ActualizarDatosCuenta(Usuario usuarioModif)
        {
            if ((String)Session["PerfilUsuario"] != null)
            {
                try
                {
                    var ln = new NegocioCuenta();
                    var aud = new Auditoria();
                    var inte = new IntegridadDatos();

                    // Traduce páginas de CUENTA.
                    TraducirPagina((String)Session["IdiomaApp"]);

                    var usrAnterior = ln.InformacionCuenta(usuarioModif.Id.ToString());

                    if (usuarioModif.Direccion == null && usuarioModif.Telefono == null && usuarioModif.Localidad.Id == 0 && usuarioModif.Idioma.Id == 0)
                    { return RedirectToAction("Index"); }

                    if (usuarioModif.Direccion == null)
                    { usuarioModif.Direccion = usrAnterior.Direccion; }


                    if (usuarioModif.Telefono == null)
                    { usuarioModif.Telefono = usrAnterior.Telefono; }


                    if (usuarioModif.Localidad.Id == 0)
                    { usuarioModif.Localidad.Id = usrAnterior.Localidad.Id; }

                    if (usuarioModif.Idioma.Id == 0)
                    { usuarioModif.Idioma.Id = usrAnterior.Idioma.Id; }

                    ln.ActualizarDatosCuenta(usuarioModif);

                    var usuarioActual = ln.BuscarUsuarioPorUsuario((String)Session["UsrLogin"]);

                    var usuarioActualDVH = inte.CalcularDVH(usuarioActual.Id.ToString() + usuarioActual.RazonSocial + usuarioActual.Nombre + usuarioActual.Apellido + usuarioActual.Usr + usuarioActual.Psw + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + usuarioActual.Telefono + usuarioActual.Direccion);

                    // Actualiza el DVH y DVV.
                    inte.ActualizarDVHUsuario(usuarioActual.Id, usuarioActualDVH);
                    inte.RecalcularDVV("SEG_Usuario");

                    aud.grabarBitacora(DateTime.Now, usuarioActual.Usr, "CAMBIO DATOS CUENTA", "INFO", "Se han actualizado datos de cuenta del Usuario: " + usuarioActual.Usr + ".");

                    Session["IdiomaApp"] = usuarioActual.Idioma.Abreviacion;

                    return RedirectToAction("Index", "Home");
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
            // Traduce páginas de CUENTA.
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();
        }

        [HttpPost]
        public ActionResult EnviarNuevaPsw(FrmOlvidoPsw formularioCambioPsw)
        {
            var negocioUsuario = new NegocioCuenta();

            // Traduce páginas de CUENTA.
            TraducirPagina((String)Session["IdiomaApp"]);

            if (negocioUsuario.ValidarUsuario(formularioCambioPsw.Usuario))
            {
                Session["ErrorRecuperoPsw"] = "El Usuario que ingreso no es inválido.";
                return RedirectToAction("RecuperarPsw");
            }

            var servicioCorreo = new Mensajeria();

            var usuarioActual = negocioUsuario.BuscarUsuarioPorUsuario(formularioCambioPsw.Usuario);

            negocioUsuario.ActualizarPswUsuario(usuarioActual.Usr, "Inicio1234");

            var asuntoMsj = "Cambio de Contraseña";
            var cuerpoMsj = "Se ha reestablecido su contraseña. La misma es 'Inicio1234'. Por favor cuando ingrese correctamente se recomienda cambiarla. Muchas gracias.";

            servicioCorreo.EnviarCorreo("implantagraf@gmail.com", usuarioActual.Email, asuntoMsj, cuerpoMsj);

            return View();
        }

        public ActionResult CambiarPsw()
        {
            // Traduce páginas de CUENTA.
            TraducirPagina((String)Session["IdiomaApp"]);

            return View();
        }


        [HttpPost]
        public ActionResult ActualizarPsw(FrmCambiarPsw frmCambioPsw)
        {
            var negocioUsuario = new NegocioCuenta();
            var servicioCorreo = new Mensajeria();

            // Traduce páginas de CUENTA.
            TraducirPagina((String)Session["IdiomaApp"]);

            negocioUsuario.ActualizarPswUsuario((String)Session["UsrLogin"], frmCambioPsw.NuevaPsw);

            var asuntoMsj = "Cambio de Contraseña";
            var cuerpoMsj = "Se ha actualizado su contraseña. Si usted no solicito este cambio por favor comuniquese con nostros. Muchas gracias.";

            servicioCorreo.EnviarCorreo("implantagraf@gmail.com", (String)Session["EmailUsuario"], asuntoMsj, cuerpoMsj);

            return View();
        }

        private void TraducirPagina(string idioma)
        {
            var traductor = new Traductor();

            // Buscar Traducciones de Idioma.
            if (idioma == null)
            { idioma = "Esp"; }

            //Devuelve el Hastable con todas las traducciones.
            var diccionario = traductor.Traducir(idioma);

            //Traduce Vistas CUENTA.
            ViewBag.CUENTA_ACTUALIZARDATOS_TITULO = diccionario["CUENTA_ACTUALIZARDATOS_TITULO"];
            ViewBag.ENTIDAD_TELEFONO = diccionario["ENTIDAD_TELEFONO"];
            ViewBag.ENTIDAD_DIRECCION = diccionario["ENTIDAD_DIRECCION"];
            ViewBag.ENTIDAD_LOCALIDAD = diccionario["ENTIDAD_LOCALIDAD"];
            ViewBag.ENTIDAD_IDIOMA = diccionario["ENTIDAD_IDIOMA"];
            ViewBag.CUENTA_ACTUALIZARDATOS_BOTON_GUARDAR = diccionario["CUENTA_ACTUALIZARDATOS_BOTON_GUARDAR"];
            ViewBag.CUENTA_ACTUALIZAR_CLAVE_LEYENDA = diccionario["CUENTA_ACTUALIZAR_CLAVE_LEYENDA"];
            ViewBag.BOTON_VOLVER = diccionario["BOTON_VOLVER"];
            ViewBag.CUENTA_BLOQUEARCUENTA_LEYENDA = diccionario["CUENTA_BLOQUEARCUENTA_LEYENDA"];
            ViewBag.CUENTA_CAMBIAR_CLAVE_TITULO = diccionario["CUENTA_CAMBIAR_CLAVE_TITULO"];
            ViewBag.ENTIDAD_NUEVA_PSW = diccionario["ENTIDAD_NUEVA_PSW"];
            ViewBag.ENTIDAD_CONFIRMACION_PSW = diccionario["ENTIDAD_CONFIRMACION_PSW"];
            ViewBag.BOTON_ACTUALIZAR_PSW = diccionario["BOTON_ACTUALIZAR_PSW"];
            ViewBag.CUENTA_DESBLOQUEARCUENTA_LEYENDA = diccionario["CUENTA_DESBLOQUEARCUENTA_LEYENDA"];
            ViewBag.CUENTA_DETALLE_TITULO = diccionario["CUENTA_DETALLE_TITULO"];
            ViewBag.CUENTA_DETALLE_DATOSPERSONALES = diccionario["CUENTA_DETALLE_DATOSPERSONALES"];
            ViewBag.ENTIDAD_RAZONSOCIAL = diccionario["ENTIDAD_RAZONSOCIAL"];
            ViewBag.ENTIDAD_CUIL = diccionario["ENTIDAD_CUIL"];
            ViewBag.ENTIDAD_USUARIO_MAIL = diccionario["ENTIDAD_USUARIO_MAIL"];
            ViewBag.ENTIDAD_NOMBRE = diccionario["ENTIDAD_NOMBRE"];
            ViewBag.ENTIDAD_APELLIDO = diccionario["ENTIDAD_APELLIDO"];
            ViewBag.CUENTA_DETALLE_DATOSCONTACTO = diccionario["CUENTA_DETALLE_DATOSCONTACTO"];
            ViewBag.CUENTA_ENVIO_CLAVE_LEYENDA = diccionario["CUENTA_ENVIO_CLAVE_LEYENDA"];
            ViewBag.BOTON_ACTUALIZAR_DATOS_CUENTA = diccionario["BOTON_ACTUALIZAR_DATOS_CUENTA"];
            ViewBag.CUENTA_DETALLE_IDIOMA = diccionario["CUENTA_DETALLE_IDIOMA"];
            ViewBag.CUENTA_LISTADO_USUARIOS = diccionario["CUENTA_LISTADO_USUARIOS"];
            ViewBag.BOTON_ALTA_USR_ADMINISTRATIVO = diccionario["BOTON_ALTA_USR_ADMINISTRATIVO"];
            ViewBag.ENTIDAD_USUARIO = diccionario["ENTIDAD_USUARIO"];
            ViewBag.ENTIDAD_FECHA_ALTA = diccionario["ENTIDAD_FECHA_ALTA"];
            ViewBag.ENTIDAD_DESCRIPCION = diccionario["ENTIDAD_DESCRIPCION"];
            ViewBag.BOTON_DESBLOQUEAR_CUENTA = diccionario["BOTON_DESBLOQUEAR_CUENTA"];
            ViewBag.BOTON_PERMISOS = diccionario["BOTON_PERMISOS"];
            ViewBag.BOTON_BLOQUEAR_CUENTA = diccionario["BOTON_BLOQUEAR_CUENTA"];
            ViewBag.BOTON_ELIMINAR_CUENTA = diccionario["BOTON_ELIMINAR_CUENTA"];
            ViewBag.CUENTA_RECUPERAR_PSW_TITULO = diccionario["CUENTA_RECUPERAR_PSW_TITULO"];
            ViewBag.BOTON_RECUPERAR_PSW = diccionario["BOTON_RECUPERAR_PSW"];
            ViewBag.CUENTA_VERPERMISOS_TITULO = diccionario["CUENTA_VERPERMISOS_TITULO"];
            ViewBag.CUENTA_VERPERMISOS_SACAR = diccionario["CUENTA_VERPERMISOS_SACAR"];
            ViewBag.CUENTA_VERPERMISOS_DAR = diccionario["CUENTA_VERPERMISOS_DAR"];
            ViewBag.CUENTA_VERPERMISOS_LEYENDA_OTORGADOS = diccionario["CUENTA_VERPERMISOS_LEYENDA_OTORGADOS"];
            ViewBag.CUENTA_VERPERMISOS_LEYENDA_RESTANTES = diccionario["CUENTA_VERPERMISOS_LEYENDA_RESTANTES"];


        }



    }
}