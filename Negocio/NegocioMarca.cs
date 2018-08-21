using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class NegocioMarca
    {
        public Marca Agregar(Marca marca)
        {
            var ad = new MarcaDAC();

            return (ad.Agregar(marca));

        }

        public void ActualizarPorId(Marca marca)
        {
            var ad = new MarcaDAC();

            ad.ActualizarPorId(marca);

        }

        public void BorrarPorId(int id)
        {
            var ad = new MarcaDAC();

            ad.BorrarPorId(id);

        }

        public void ListarPorId(int id)
        {
            var ad = new MarcaDAC();

            ad.ListarPorId(id);

        }

        public List<Marca> Listar()
        {
            var ad = new MarcaDAC();

            return (ad.Listar());

        }


    }
}