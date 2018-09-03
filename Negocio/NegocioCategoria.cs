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
        public Categoria Agregar(Categoria categoria)
        {
            var ad = new CategoriaDAC();

            var nuevaCategoria = ad.Agregar(categoria);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var DVHBitacora = inte.CalcularDVH(categoria.Descripcion + "ALTA CATEGORIA" + "INFO");

            aud.grabarBitacora(DateTime.Now, categoria.Descripcion, "ALTA CATEGORIA", "INFO", DVHBitacora);

            return (nuevaCategoria);

        }

        public void ActualizarPorId(Categoria categoria)
        {
            var ad = new CategoriaDAC();

            ad.ActualizarPorId(categoria);

            if (categoria.Descripcion != null)
            {
                var inte = new IntegridadDatos();
                var aud = new Auditoria();

                var DVHBitacora = inte.CalcularDVH(categoria.Descripcion + "MODIFICACION CATEGORIA" + "INFO");

                aud.grabarBitacora(DateTime.Now, categoria.Id + "-" + categoria.Descripcion, "MODIFICACION CATEGORIA", "INFO", DVHBitacora);
            }
        }


        public void BorrarPorId(int id)
        {
            var ad = new CategoriaDAC();

            ad.BorrarPorId(id);

            var inte = new IntegridadDatos();

            var aud = new Auditoria();

            var DVHBitacora = inte.CalcularDVH(id.ToString() + "ELIMINO CATEGORIA" + "INFO");

            aud.grabarBitacora(DateTime.Now, id.ToString(), "ELIMINO CATEGORIA", "INFO", DVHBitacora);

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