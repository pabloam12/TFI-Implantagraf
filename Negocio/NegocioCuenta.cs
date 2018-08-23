using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class NegocioCuenta
    {
        public Usuario Registrar(Usuario usr)
        {
            var ad = new CuentaDAC();
            //var seg = new 
            //var DVH = 
            return (ad.Registrar(usr, 1321231321));

        }

        public Usuario Autenticar(Usuario usr)
        {
            var ad = new CuentaDAC();

            return (ad.Autenticar(usr));

        }
    }
}