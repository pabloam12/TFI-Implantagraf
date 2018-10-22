using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioFormaPago
    {
       public IEnumerable<FormaPago> Listar()
        {
            var ad = new FormaPagoDAC();

            return (ad.Listar());

        }

    }
}