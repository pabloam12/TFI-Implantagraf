using AccesoDatos;
using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class Traductor
    {

        public Hashtable Traducir(string idioma)
        {

            var accDatosIdioma = new IdiomaDAC();

                        
            return accDatosIdioma.Traducir(idioma);

        }

    }
}