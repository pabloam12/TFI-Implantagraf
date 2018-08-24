using AccesoDatos;
using Entidades;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class NegocioCuenta
    {
        public Usuario RegistrarCliente(Usuario usr)
        {
            var ad = new CuentaDAC();
            var aud = new Auditoria();

            usr = ad.RegistrarCliente(usr, 1321231321);

            aud.grabarBitacora(DateTime.Now, usr.Usr, "ALTA CLIENTE", "EXITO", 1212121212);

            return (usr);

        }

        public Usuario Autenticar(Usuario usr)
        {
            var ad = new CuentaDAC();

            return (ad.Autenticar(usr));

        }

        public List<Informacion> informacionCuenta(int id)
        {
            var ad = new CuentaDAC();

            return ad.informacionCuenta(id);

        }
    }
}