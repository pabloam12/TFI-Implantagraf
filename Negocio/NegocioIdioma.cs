using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioIdioma
    {
        public Idioma Agregar(Idioma idioma)
        {
            var ad = new IdiomaDAC();

            var nuevoIdioma = ad.Agregar(idioma);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var DVHBitacora = inte.CalcularDVH(idioma.Descripcion + "ALTA IDIOMA" + "INFO");

            aud.grabarBitacora(DateTime.Now, idioma.Descripcion, "ALTA IDIOMA", "INFO", DVHBitacora);

            return (nuevoIdioma);

        }

        public void ActualizarPorId(Idioma idioma)
        {
            var ad = new IdiomaDAC();

            ad.ActualizarPorId(idioma);

            if (idioma.Descripcion != null)
            {
                var inte = new IntegridadDatos();
                var aud = new Auditoria();

                var DVHBitacora = inte.CalcularDVH(idioma.Descripcion + "MODIFICACION CATEGORIA" + "INFO");

                aud.grabarBitacora(DateTime.Now, idioma.Id + "-" + idioma.Descripcion, "MODIFICACION CATEGORIA", "INFO", DVHBitacora);
            }

        }

        public void BorrarPorId(int id)
        {
            var ad = new IdiomaDAC();

            ad.BorrarPorId(id);

            var inte = new IntegridadDatos();

            var aud = new Auditoria();

            var DVHBitacora = inte.CalcularDVH(id.ToString() + "ELIMINO IDIOMA" + "INFO");

            aud.grabarBitacora(DateTime.Now, id.ToString(), "ELIMINO IDIOMA", "INFO", DVHBitacora);

        }

        public Idioma ListarPorId(int id)
        {
            var ad = new IdiomaDAC();

            return (ad.ListarPorId(id));

        }

        public List<Idioma> Listar()
        {
            var ad = new IdiomaDAC();

            return (ad.Listar());

        }


    }
}