using AccesoDatos;
using Entidades;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Usuario> ListarUsuariosPorPerfil(int perfil)
        {

            var ad = new CuentaDAC();

            return ad.ListarUsuariosPorPerfil(perfil);


        }

        public List<PermisosUsr> VerPermisosUsuario(int usuarioId)
        {

            var ad = new CuentaDAC();

            return ad.VerPermisosUsuario(usuarioId);

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
                      
            var usuarioActual = ad.RegistrarUsuario(usr);

            var clienteDVH = inte.CalcularDVH(usuarioActual.Id.ToString() + usuarioActual.RazonSocial + usuarioActual.Nombre + usuarioActual.Apellido + usuarioActual.Usr + usuarioActual.Psw + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + usuarioActual.Telefono + usuarioActual.Direccion);

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHUsuario(usuarioActual.Id, clienteDVH);
            inte.RecalcularDVV("SEG_Usuario");

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, "SISTEMA", "ALTA USUARIO", "INFO", "Se registró al usuario: " + usuarioActual.Id.ToString() + " - '" + usuarioActual.Usr + "' con el perfil de " + usuarioActual.PerfilUsr.Descripcion);

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
    }
}