using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;

namespace Negocio
{
    public class NegocioIdioma
    {
        public Idioma Agregar(Idioma idioma)
        {
            var ad = new IdiomaDAC();

            return (ad.Agregar(idioma));

        }

        public void ActualizarPorId(Idioma idioma)
        {
            var ad = new IdiomaDAC();

            ad.ActualizarPorId(idioma);

        }

        public void BorrarPorId(int id)
        {
            var ad = new IdiomaDAC();

            ad.BorrarPorId(id);

        }

        public void ListarPorId(int id)
        {
            var ad = new IdiomaDAC();

            ad.ListarPorId(id);

        }

        public List<Idioma> Listar()
        {
            var ad = new IdiomaDAC();

            return (ad.Listar());

        }


    }
}