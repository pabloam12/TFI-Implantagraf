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
        public Localidad Agregar(Localidad localidad)
        {
            var ad = new LocalidadDAC();

            return (ad.Agregar(localidad));

        }

        public void ActualizarPorId(Localidad localidad)
        {
            var ad = new LocalidadDAC();

            ad.ActualizarPorId(localidad);

        }

        public void BorrarPorId(int id)
        {
            var ad = new LocalidadDAC();

            ad.BorrarPorId(id);

        }

        public Localidad ListarPorId(int id)
        {
            var ad = new LocalidadDAC();

            return ad.ListarPorId(id);

        }

        public List<Localidad> Listar()
        {
            var ad = new LocalidadDAC();

            return (ad.Listar());

        }


    }
}