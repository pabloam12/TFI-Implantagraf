using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;

namespace Negocio
{
    public class NegocioLocalidad
    {
        public void Agregar(Localidad localidad)
        {
            var ad = new LocalidadDAC();

            ad.Agregar(localidad);

              }
    }
}