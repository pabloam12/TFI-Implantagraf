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

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "ALTA CATEGORIA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA CATEGORIA", "INFO", "Se creó la categoría : '" + categoria.Descripcion + "'", BitacoraDVH);

            return (nuevaCategoria);

        }

        public void ActualizarPorId(Categoria categoria, string usuario)
        {
            var ad = new CategoriaDAC();

            var descripcionAnterior = BuscarPorId(categoria.Id).Descripcion;

            ad.ActualizarPorId(categoria);

            if (categoria.Descripcion != null)
            {
                var inte = new IntegridadDatos();
                var aud = new Auditoria();

                var BitacoraDVH = inte.CalcularDVH(usuario + "MODIFICAR CATEGORIA" + "INFO");

                aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR CATEGORIA", "INFO", "Se actualizó la categoría: " + categoria.Id + " - '" + descripcionAnterior + "' a '" + categoria.Descripcion + "'", BitacoraDVH);
            }
        }


        public void BorrarPorId(Categoria categoria, string usuario)
        {
            var ad = new CategoriaDAC();

            ad.BorrarPorId(categoria.Id);

            var inte = new IntegridadDatos();

            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "ELIMINAR CATEGORIA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "ELIMINAR CATEGORIA", "INFO", "Se eliminó la categoría: " + categoria.Id.ToString() + " -" + categoria.Descripcion, BitacoraDVH);

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