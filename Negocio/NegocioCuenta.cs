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
            var seg = new Privacidad();

            usr.Psw = seg.EncriptarPsw(usr.Psw);

            usr = ad.RegistrarCliente(usr, 1321231321);

            aud.grabarBitacora(DateTime.Now, usr.Usr, "ALTA CLIENTE", "INFO", 1212121212);

            return (usr);

        }

        public void RegistrarUsuario(Usuario usr, int perfil, int idioma, int localidad)
        {
            var ad = new CuentaDAC();
            var aud = new Auditoria();
            var seg = new Privacidad();

            var DVH = 121312321;

            usr.Psw = seg.EncriptarPsw(usr.Psw);

            ad.RegistrarUsuario(usr, perfil, idioma, localidad, DVH);

            aud.grabarBitacora(DateTime.Now, usr.Usr, "ALTA USUARIO", "INFO", DVH);

        }

        public Usuario Autenticar(Usuario usr)
        {
            var ad = new CuentaDAC();

            return ad.Autenticar(usr);

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