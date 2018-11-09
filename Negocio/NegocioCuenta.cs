using AccesoDatos;
using Entidades;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Negocio
{
    public class NegocioCuenta
    {

        public List<Usuario> ListarUsuarios()
        {
            var ad = new CuentaDAC();

            return (ad.ListarUsuarios());

        }

        public Usuario BuscarUsuarioPorUsuario(string usr)
        {
            var ad = new CuentaDAC();

            return (ad.BuscarUsuarioPorUsuario(usr));

        }
        public List<Usuario> ListarUsuariosPorPerfil(int perfil)
        {

            var ad = new CuentaDAC();

            return ad.ListarUsuariosPorPerfil(perfil);


        }

        public List<DetallePermisoUsr> VerDetallePermisosUsuario(int usuarioId)
        {

            var ad = new CuentaDAC();

            return ad.ListarDetallePermisosPorUsuario(usuarioId);

        }

        public DetallePermisoUsr DarPermiso(int detallePermisoId)
        {

            var ad = new CuentaDAC();
            var integ = new IntegridadDatos();
            var aud = new Auditoria();

            ad.ActualizarPermiso(detallePermisoId, "S");

            var detallePermisoActual = ad.BuscarDetallePermisoPorId(detallePermisoId);

            //Recalculo Digitos Verificadores.
            var DVH = integ.CalcularDVH(detallePermisoActual.Id.ToString() + detallePermisoActual.UsrId.ToString() + detallePermisoActual.PermisoId.ToString() + detallePermisoActual.Otorgado);
            integ.ActualizarDVHDetallePermisos(detallePermisoActual.Id, DVH);
            integ.RecalcularDVV("SEG_DetallePermisos");

            aud.grabarBitacora(DateTime.Now, "admin", "CAMBIO PERMISO", "INFO", "Se cambió el permiso: " + detallePermisoActual.Descripcion);


            return detallePermisoActual;

        }

        public DetallePermisoUsr SacarPermiso(int detallePermisoId)
        {

            var ad = new CuentaDAC();
            var integ = new IntegridadDatos();
            var aud = new Auditoria();

            ad.ActualizarPermiso(detallePermisoId, "N");

            var detallePermisoActual = ad.BuscarDetallePermisoPorId(detallePermisoId);

            //Recalculo Digitos Verificadores.
            var DVH = integ.CalcularDVH(detallePermisoActual.Id.ToString() + detallePermisoActual.UsrId.ToString() + detallePermisoActual.PermisoId.ToString() + detallePermisoActual.Otorgado);
            integ.ActualizarDVHDetallePermisos(detallePermisoActual.Id, DVH);
            integ.RecalcularDVV("SEG_DetallePermisos");

            aud.grabarBitacora(DateTime.Now, "admin", "CAMBIO PERMISO", "INFO", "Se cambió el permiso: " + detallePermisoActual.Descripcion);

            
            return detallePermisoActual;

        }

        public void OtorgarPermisosWebmaster(int usuarioId)
        {
            var ad = new CuentaDAC();
            var integ = new IntegridadDAC();

            ad.OtorgarPermisosWebmaster(usuarioId);

            // DetallePermisos
            var listadoDetallePermisos = ad.ListarDetallePermisosPorUsuario(usuarioId);


            foreach (DetallePermisoUsr detallePermisoActual in listadoDetallePermisos)
            {
                var dvhDetallePermisoUsrActual = CalcularDVH(detallePermisoActual.Id.ToString() + detallePermisoActual.UsrId.ToString() + detallePermisoActual.PermisoId.ToString() + detallePermisoActual.Otorgado);

                integ.ActualizarDVHDetallePermisos(detallePermisoActual.Id, dvhDetallePermisoUsrActual);
            }

            RecalcularDVV("SEG_DetallePermisos");

        }

        public void OtorgarPermisosCliente(int usuarioId)
        {
            var ad = new CuentaDAC();
            var integ = new IntegridadDAC();

            ad.OtorgarPermisosCliente(usuarioId);

            // DetallePermisos
            var listadoDetallePermisos = ad.ListarDetallePermisosPorUsuario(usuarioId);


            foreach (DetallePermisoUsr detallePermisoActual in listadoDetallePermisos)
            {
                var dvhDetallePermisoUsrActual = CalcularDVH(detallePermisoActual.Id.ToString() + detallePermisoActual.UsrId.ToString() + detallePermisoActual.PermisoId.ToString() + detallePermisoActual.Otorgado);

                integ.ActualizarDVHDetallePermisos(detallePermisoActual.Id, dvhDetallePermisoUsrActual);
            }

            RecalcularDVV("SEG_DetallePermisos");

        }

        public void OtorgarPermisosAdministrativo(int usuarioId)
        {
            var ad = new CuentaDAC();
            var integ = new IntegridadDAC();

            ad.OtorgarPermisosAdministrativo(usuarioId);

            // DetallePermisos
            var listadoDetallePermisos = ad.ListarDetallePermisosPorUsuario(usuarioId);


            foreach (DetallePermisoUsr detallePermisoActual in listadoDetallePermisos)
            {
                var dvhDetallePermisoUsrActual = CalcularDVH(detallePermisoActual.Id.ToString() + detallePermisoActual.UsrId.ToString() + detallePermisoActual.PermisoId.ToString() + detallePermisoActual.Otorgado);

                integ.ActualizarDVHDetallePermisos(detallePermisoActual.Id, dvhDetallePermisoUsrActual);
            }

            RecalcularDVV("SEG_DetallePermisos");

        }

        public long CalcularDVH(string cadena)
        {
            var DVH = 0;
            var I = 1;

            if (cadena == null || cadena == "")
            {
                return DVH;
            }

            for (I = 1; I <= cadena.Length; I++)
            {
                DVH = DVH + Encoding.ASCII.GetBytes(cadena.Substring(I - 1, 1))[0];
            }

            return DVH;
        }

        public void RecalcularDVV(string tabla)
        {
            var integ = new IntegridadDAC();

            if (integ.ExisteRegTablaDVV(tabla) != 0 && integ.ValidarExistencia(tabla) != 0)
            {
                var DVV = integ.CalcularDVV(tabla);

                var cantReg = integ.ContarRegistros(tabla);

                integ.ActualizarDVV(tabla, DVV, cantReg);
            }
        }


        public void BloquearCuenta(int usuarioId)
        {

            var ad = new CuentaDAC();

            ad.BloquearCuenta(usuarioId);


        }

        public void DesbloquearCuenta(int usuarioId)
        {

            var ad = new CuentaDAC();

            ad.DesbloquearCuenta(usuarioId);


        }

        public Usuario RegistrarUsuario(Usuario usr)
        {
            var ad = new CuentaDAC();
            var aud = new Auditoria();
            var priv = new Privacidad();
            var inte = new IntegridadDatos();

            usr.Psw = priv.EncriptarPsw(usr.Psw);

            //TODO CIFRAR DATOS DE USR.

            var usuarioActual = ad.RegistrarUsuario(usr);

            var usuarioActualDVH = inte.CalcularDVH(usuarioActual.Id.ToString() + usuarioActual.RazonSocial + usuarioActual.Nombre + usuarioActual.Apellido + usuarioActual.Usr + usuarioActual.Psw + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + usuarioActual.Telefono + usuarioActual.Direccion);

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHUsuario(usuarioActual.Id, usuarioActualDVH);
            inte.RecalcularDVV("SEG_Usuario");

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, "SISTEMA", "ALTA USUARIO", "INFO", "Se registró al Usuario: " + usuarioActual.Id.ToString() + " - '" + usuarioActual.Usr + "' con el perfil de " + usuarioActual.PerfilUsr.Descripcion);

            return (usuarioActual);

        }

        public Usuario Autenticar(FrmLogin usr)
        {
            var ad = new CuentaDAC();

            var aud = new Auditoria();
            var inte = new IntegridadDatos();

            var usrLogin = ad.Autenticar(usr);

            aud.grabarBitacora(DateTime.Now, usrLogin.Usr, "INICIO DE SESIÓN", "INFO", "El Usuario ha inciado sesión en el sistema.");

            return usrLogin;

        }

        public Usuario InformacionCuenta(string idUsuario)
        {
            var ad = new CuentaDAC();

            return ad.InformacionCuenta(idUsuario);

        }


        public bool ValidarUsuario(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            return ad.ValidarUsuario(nombreUsuario);

        }

        public bool ValidarBloqueoCuenta(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            return ad.ValidarBloqueoCuenta(nombreUsuario);

        }

        public bool ValidarSesionActiva(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            return ad.ValidarSesionActiva(nombreUsuario);

        }

        public void ActualizarDatosCuenta(Usuario usuarioModif)
        {
            var ad = new CuentaDAC();

            ad.ActualizarDatosCuenta(usuarioModif);

        }

        public bool ValidarUsuarioPsw(string nombreUsuario, string pswUsuario)
        {
            var ad = new CuentaDAC();

            return ad.ValidarUsuarioPsw(nombreUsuario, pswUsuario);

        }

        public void BloquearCuentaUsuario(string nombreUsuario)
        {
            var ad = new CuentaDAC();
            var aud = new Auditoria();

            // Bloquea la cuenta de Usuario
            ad.BloquearCuentaUsuario(nombreUsuario);

            aud.grabarBitacora(DateTime.Now, nombreUsuario, "BLOQUEO DE CUENTA", "INFO", "Se ha bloqueado la cuenta de Usuario.");

        }

        public void ActivarSesionCuentaUsuario(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            // Inicio Sesion de cuenta de Usuario
            ad.ActivarSesionCuentaUsuario(nombreUsuario);

        }

        public void ActivarCuentaUsuario(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            // Inicio Sesion de cuenta de Usuario
            ad.ActivarCuentaUsuario(nombreUsuario);

        }

        public int SumarIntentoFallido(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            return ad.SumarIntentoFallido(nombreUsuario);

        }

        public void ReiniciarIntentosFallidos(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            ad.ReiniciarIntentosFallidos(nombreUsuario);
        }

        public void ActualizarPswUsuario(string usr, string nuevaPsw)
        {
            var priv = new Privacidad();
            var ad = new CuentaDAC();
            var inte = new IntegridadDatos();
            var negocioUsr = new NegocioCuenta();
            var aud = new Auditoria();

            var nuevaPswEncriptada = priv.EncriptarPsw(nuevaPsw);

            ad.ActualizarPswUsuario(usr, nuevaPswEncriptada);

            var usuarioActual = negocioUsr.BuscarUsuarioPorUsuario(usr);

            var usuarioActualDVH = inte.CalcularDVH(usuarioActual.Id.ToString() + usuarioActual.RazonSocial + usuarioActual.Nombre + usuarioActual.Apellido + usuarioActual.Usr + usuarioActual.Psw + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + usuarioActual.Telefono + usuarioActual.Direccion);

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHUsuario(usuarioActual.Id, usuarioActualDVH);
            inte.RecalcularDVV("SEG_Usuario");

            aud.grabarBitacora(DateTime.Now, usr, "CAMBIO CLAVE", "INFO", "Se ha cambiado la contraseña del Usuario: " + usr + ".");

        }
    }
}