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

        public List<Usuario> ListarUsuariosPorPerfil(int perfil)
        {

            var ad = new CuentaDAC();

            return ad.ListarUsuariosPorPerfil(perfil);


        }
        public Usuario RegistrarCliente(Usuario usr)
        {
            var ad = new CuentaDAC();
            var aud = new Auditoria();
            var priv = new Privacidad();
            var inte = new IntegridadDatos();

            usr.Psw = priv.EncriptarPsw(usr.Psw);

            var clienteDVH = inte.CalcularDVH(usr.RazonSocial + usr.CUIL + "3" + usr.Email + usr.Psw);

            usr = ad.RegistrarCliente(usr, clienteDVH);
            
            inte.RecalcularDVV("SEG_Usuario");

            var BitacoraDVH = inte.CalcularDVH("SISTEMA" + usr.Usr + "ALTA USUARIO" + "INFO");

            aud.grabarBitacora(DateTime.Now, usr.Usr, "ALTA USUARIO", "INFO", "Se registró al usuario: " + usr.Id + " - '" + usr.Usr + "' con el perfil de 'Cliente'", BitacoraDVH);

            return (usr);

        }

        public void RegistrarUsuario(Usuario usr, int perfil, int idioma, int localidad, String usuarioSistema)
        {
            var ad = new CuentaDAC();
            var aud = new Auditoria();
            var seg = new Privacidad();

            var priv = new Privacidad();
            var inte = new IntegridadDatos();

            usr.Psw = priv.EncriptarPsw(usr.Psw);

            var usuarioDVH = inte.CalcularDVH(usr.RazonSocial + usr.CUIL + perfil.ToString() + usr.Usr + usr.Psw);
            
            ad.RegistrarUsuario(usr, perfil, idioma, localidad, usuarioDVH);

            inte.RecalcularDVV("SEG_Usuario");

            var BitacoraDVH = inte.CalcularDVH(usuarioSistema + usr.Usr + "ALTA USUARIO" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuarioSistema, "ALTA USUARIO", "INFO", "Se registró al usuario: " + usr.Id + " - '" + usr.Usr + "' con el perfil de '" + perfil + "'", BitacoraDVH);

        }

        public Usuario Autenticar(Login usr)
        {
            var ad = new CuentaDAC();

            var aud = new Auditoria();
            var inte = new IntegridadDatos();

            var usrLogin = ad.Autenticar(usr);

            var BitacoraDVH = inte.CalcularDVH(usrLogin.Usr + "LOGIN DE USUARIO" + "INFO");

            aud.grabarBitacora(DateTime.Now, usrLogin.Usr, "INICIO DE SESIÓN", "INFO", "El Usuario ha inciado sesión en el sistema.", BitacoraDVH);

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

            // Bloquea la cuenta de Usuario
            ad.BloquearCuentaUsuario(nombreUsuario);

            var seg = new Privacidad();

            var aud = new Auditoria();
            var inte = new IntegridadDatos();

            var BitacoraDVH = inte.CalcularDVH(nombreUsuario + "BLOQUEO DE CUENTA" + "INFO");

            aud.grabarBitacora(DateTime.Now, nombreUsuario, "BLOQUEO DE CUENTA", "INFO", "Se ha bloqueado la cuenta por ingresos erroneos.", BitacoraDVH);

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