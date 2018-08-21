using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;

namespace Negocio
{
    public class NegocioCategoria
    {
        public Categoria Agregar(Categoria categoria)
        {
            var ad = new CategoriaDAC();

            return (ad.Agregar(categoria));

        }

        public void ActualizarPorId(Categoria categoria)
        {
            var ad = new CategoriaDAC();

            ad.ActualizarPorId(categoria);

        }

        public void BorrarPorId(int id)
        {
            var ad = new CategoriaDAC();

            ad.BorrarPorId(id);

        }

        public void ListarPorId(int id)
        {
            var ad = new CategoriaDAC();

            ad.ListarPorId(id);

        }

        public List<Categoria> Listar()
        {
            var ad = new CategoriaDAC();

            return (ad.Listar());

        }


    }
}