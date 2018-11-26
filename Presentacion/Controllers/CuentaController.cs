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
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] != null && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                return RedirectToAction("DetalleCuenta");
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult ListarUsuarios()
        {

            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                try
                {
                    var ln = new NegocioCuenta();

                    // Traduce páginas de CUENTA.
                    TraducirPagina((String)Session["IdiomaApp"]);

                    return View(ln.ListarUsuarios());

                }
                catch
                {
                    Session["Excepcion"] = "ERROR AL CONSULTAR USUARIOS";
                    return RedirectToAction("Index", "Excepciones");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MisCompras()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] != "WebMaster" && (String)Session["PerfilUsuario"] != null && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                try
                {
                    var ln = new NegocioOperaciones();

                    // Traduce páginas de CUENTA.
                    TraducirPagina((String)Session["IdiomaApp"]);

                    var codCliente = (String)Session["IdUsuario"];

                    return View(ln.ListarVentasPorCliente(codCliente));
                }
                catch
                {
                    Session["Excepcion"] = "ERROR AL CONSULTAR COMPRAS";
                    return RedirectToAction("Index", "Excepciones");
                }
            }

            return RedirectToAction("Index", "Home");
        }



        public ActionResult DetalleCuenta()
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] != null && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                try
                {
                    var ln = new NegocioCuenta();

                    var idUsuario = (String)Session["IdUsuario"];

                    // Traduce páginas de CUENTA.
                    TraducirPagina((String)Session["IdiomaApp"]);

                    return View(ln.InformacionCuenta(idUsuario));

                }
                catch
                {
                    Session["Excepcion"] = "ERROR AL CONSULTAR DETALLE DE CUENTA";
                    return RedirectToAction("Index", "Excepciones");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerPermisosUsuario(int idUsr)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Permisos") == 1 && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                try
                {
                    var ln = new NegocioCuenta();

                    // Traduce páginas de CUENTA.
                    TraducirPagina((String)Session["IdiomaApp"]);

                    return View(ln.VerDetallePermisosUsuario(idUsr));
                }
                catch
                {
                    Session["Excepcion"] = "ERROR AL CONSULTAR PERMISOS DE USUARIO";
                    return RedirectToAction("Index", "Excepciones");
                }
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult SacarPermiso(int idDetallePermiso)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                var ln = new NegocioCuenta();

                var detalleModificado = ln.SacarPermiso(idDetallePermiso);

                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "PERMISOS", "INFO", "El usuario ha quitado el permiso: " + idDetallePermiso.ToString());


                return RedirectToAction("VerPermisosUsuario", new { idUsr = detalleModificado.UsrId });
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DarPermiso(int idDetallePermiso)
        {

            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_DetallePermisos") == 1)
            {

                var ln = new NegocioCuenta();

                var detalleModificado = ln.DarPermiso(idDetallePermiso);

                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "PERMISOS", "INFO", "El usuario ha otorgado el permiso: " + idDetallePermiso.ToString());



                return RedirectToAction("VerPermisosUsuario", new { idUsr = detalleModificado.UsrId });

            }

            return RedirectToAction("Index", "Home");
        }



        public ActionResult BloquearCuenta(int id)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                var ln = new NegocioCuenta();

                ln.BloquearCuenta(id);

                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "BLOQUEO", "INFO", "El usuario ha bloqueado la cuenta: " + id.ToString());



                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult DesbloquearCuenta(int id)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] == "WebMaster" && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                var ln = new NegocioCuenta();

                ln.DesbloquearCuenta(id);

                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                var aud = new Auditoria();
                aud.grabarBitacora(DateTime.Now, (String)Session["UsrLogin"], "DESBLOQUEO", "INFO", "El usuario ha desbloqueado la cuenta: " + id.ToString());


                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult ReenviarFactura(int facturaId)
        {
            try
            {
                var integ = new IntegridadDatos();
                var mensajeria = new Mensajeria();

                var rutaFactura = "C:\\Implantagraf\\PDF\\factura_" + facturaId.ToString() + ".pdf";
                var cuerpoMsj = ViewBag.MENSAJE_MAIL_COMPRA;
                var asuntoMsj = "F-000" + facturaId.ToString();
                mensajeria.EnviarCorreo("implantagraf@gmail.com", (String)Session["EmailUsuario"], asuntoMsj, cuerpoMsj, rutaFactura);

                return RedirectToAction("MisCompras", "Cuenta");
            }
            catch
            {
                Session["Excepcion"] = "ERROR AL REENVIAR FACTURA POR CORREO";
                return RedirectToAction("Index", "Excepciones");
            }
        }

        public ActionResult ActualizarDatosCuenta(Usuario usuarioModif)
        {
            var integ = new IntegridadDatos();

            if ((String)Session["PerfilUsuario"] != null && integ.ValidarExistencia("SEG_Usuario") == 1)
            {
                try
                {
                    var ln = new NegocioCuenta();
                    var aud = new Auditoria();
                    var inte = new IntegridadDatos();
                    var priv = new Privacidad();

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

                    //Actualizo datos.
                    ln.ActualizarDatosCuenta(usuarioModif);

                    var usuarioActual = ln.BuscarUsuarioPorUsuario((String)Session["UsrLogin"]);

                    var usuarioActualDVH = inte.CalcularDVH(usuarioActual.Id.ToString() + priv.Cifrar(usuarioActual.RazonSocial) + priv.Cifrar(usuarioActual.Nombre) + priv.Cifrar(usuarioActual.Apellido) + priv.Cifrar(usuarioActual.Usr) + priv.Cifrar(usuarioActual.Psw) + priv.Cifrar(usuarioActual.CUIL) + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + priv.Cifrar(usuarioActual.Telefono) + priv.Cifrar(usuarioActual.Direccion));

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
            if ((String)Session["PerfilUsuario"] == null)
            {
                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult EnviarNuevaPsw(FrmOlvidoPsw formularioCambioPsw)
        {
            if (formularioCambioPsw.Usuario == null)
            { formularioCambioPsw.Usuario = formularioCambioPsw.Usuario_Eng; }

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

            var aud = new Auditoria();
            aud.grabarBitacora(DateTime.Now, usuarioActual.Usr, "BLANQUEO PSW", "INFO", "El usuario ha pedido nueva clave.");


            try
            {
                var asuntoMsj = "Cambio de Contraseña";
                var cuerpoMsj = "Se ha reestablecido su contraseña. La misma es 'Inicio1234'. Por favor cuando ingrese correctamente se recomienda cambiarla. Muchas gracias.";

                servicioCorreo.EnviarCorreo("implantagraf@gmail.com", usuarioActual.Email, asuntoMsj, cuerpoMsj);
            }
            catch
            { //TODO
            }

            return View();
        }

        public ActionResult CambiarPsw()
        {
            if ((String)Session["PerfilUsuario"] != null)
            {
                // Traduce páginas de CUENTA.
                TraducirPagina((String)Session["IdiomaApp"]);

                return View();
            }

            return RedirectToAction("Index", "Home");
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

            ViewBag.TITULO_COMPRAS = diccionario["TITULO_COMPRAS"];
            ViewBag.BOTON_REENVIAR_FACTURA = diccionario["BOTON_REENVIAR_FACTURA"];
            ViewBag.BOTON_REENVIAR_FACTURA = diccionario["MENSAJE_MAIL_COMPRA"]; 


        }



    }
}