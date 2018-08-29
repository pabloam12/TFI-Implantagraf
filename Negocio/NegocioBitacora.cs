using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;

namespace Negocio
{
    public class NegocioBitacora
    {
       public List<Bitacora> ConsultarBitacora()
        {
            var ad = new BitacoraDAC();

            return (ad.ConsultarBitacora());

        }


    }
}