using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioMarcaTC
    {
        
        public IEnumerable<Marca_TC> Listar()
        {
            var ad = new Marca_TC_DAC();

            return (ad.Listar());

        }


    }
}