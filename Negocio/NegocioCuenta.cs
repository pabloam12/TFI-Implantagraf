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
        public void Registrar(Usuario usr)
        {


        }

        public Usuario Autenticar(Usuario usr)
        {
            var ad = new CuentaDAC();

            return (ad.Autenticar(usr));

        }
    }
}