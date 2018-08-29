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
        public Usuario RegistrarCliente(Usuario usr)
        {
            var ad = new CuentaDAC();
            var aud = new Auditoria();
            var seg = new Privacidad();

            usr.Psw = seg.EncriptarPsw(usr.Psw);

            usr = ad.RegistrarCliente(usr, 1321231321);

            aud.grabarBitacora(DateTime.Now, usr.Usr, "ALTA CLIENTE", "INFO", 1212121212);

            return (usr);

        }

        public Usuario Autenticar(Usuario usr)
        {
            var ad = new CuentaDAC();

            return ad.Autenticar(usr);
            
        }

        public Usuario informacionCuenta(string idUsuario)
        {
            var ad = new CuentaDAC();

            return ad.informacionCuenta(idUsuario);

        }


        public bool ValidarUsuario (string nombreUsuario)
        {
            var ad = new CuentaDAC();

            return ad.ValidarUsuario(nombreUsuario);

        }

        public bool ValidarBloqueoCuenta(string nombreUsuario)
        {
            var ad = new CuentaDAC();

            return ad.ValidarBloqueoCuenta(nombreUsuario);

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

            ad.BloquearCuentaUsuario(nombreUsuario);

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