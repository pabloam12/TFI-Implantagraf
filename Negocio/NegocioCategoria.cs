using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioCategoria
    {
        public Categoria Agregar(Categoria categoria, string usuario)
        {
            var ad = new CategoriaDAC();

            var nuevaCategoria = ad.Agregar(categoria);

            var aud = new Auditoria();

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA CATEGORIA", "INFO", "Se creó la categoría : '" + categoria.Descripcion + "'");

            return (nuevaCategoria);

        }

        public void ActualizarPorId(Categoria categoria, string usuario)
        {
            var ad = new CategoriaDAC();

            var descripcionAnterior = BuscarPorId(categoria.Id).Descripcion;

            ad.ActualizarPorId(categoria);

            if (categoria.Descripcion != null)
            {
                var aud = new Auditoria();

                aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR CATEGORIA", "INFO", "Se actualizó la categoría: " + categoria.Id + " - '" + descripcionAnterior + "' a '" + categoria.Descripcion + "'");
            }
        }


        public void BorrarPorId(Categoria categoria, string usuario)
        {
            var ad = new CategoriaDAC();

            ad.BorrarPorId(categoria.Id);

            var aud = new Auditoria();
            
            aud.grabarBitacora(DateTime.Now, usuario, "ELIMINAR CATEGORIA", "INFO", "Se eliminó la categoría: " + categoria.Id.ToString() + " -" + categoria.Descripcion);

        }

        public Categoria BuscarPorId(int id)
        {
            var ad = new CategoriaDAC();

            return ad.BuscarPorId(id);

        }

        public IEnumerable<Categoria> Listar()
        {
            var ad = new CategoriaDAC();

            return (ad.Listar());

        }

    }
}