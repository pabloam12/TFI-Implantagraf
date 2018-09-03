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

            var ClienteDVH = inte.CalcularDVH(usr.RazonSocial+usr.Psw+usr.CUIL+usr.CUIL); 

            usr = ad.RegistrarCliente(usr, ClienteDVH);

            var BitacoraDVH = inte.CalcularDVH(usr.Usr + "ALTA CLIENTE" + "INFO");

            aud.grabarBitacora(DateTime.Now, usr.Usr, "ALTA CLIENTE", "INFO", BitacoraDVH);

            return (usr);

        }

        public void RegistrarUsuario(Usuario usr, int perfil, int idioma, int localidad)
        {
                            var ad = new CuentaDAC();
                var aud = new Auditoria();
                var seg = new Privacidad();

                var priv = new Privacidad();
                var inte = new IntegridadDatos();
                usr.Psw = priv.EncriptarPsw(usr.Psw);

                var UsuarioDVH = inte.CalcularDVH(usr.RazonSocial + usr.Psw + usr.CUIL + usr.CUIL);
                ad.RegistrarUsuario(usr, perfil, idioma, localidad, UsuarioDVH);
                var BitacoraDVH = inte.CalcularDVH(usr.Usr + "ALTA CLIENTE" + "INFO");

                aud.grabarBitacora(DateTime.Now, usr.Usr, "ALTA CLIENTE", "INFO", BitacoraDVH);
                       

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