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
        public Idioma Agregar(Idioma idioma, string usuario)
        {
            var ad = new IdiomaDAC();

            idioma = ad.Agregar(idioma);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var DVHBitacora = inte.CalcularDVH(idioma.Descripcion + "ALTA IDIOMA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA IDIOMA", "INFO", "Se creó el idioma: " + idioma.Id + " - " + idioma.Descripcion, DVHBitacora);

            return (idioma);

        }

        public void ActualizarPorId(Idioma idioma, string usuario)
        {
            var ad = new IdiomaDAC();

            var descripcionAnterior = BuscarPorId(idioma.Id).Descripcion;

            ad.ActualizarPorId(idioma);

            if (idioma.Descripcion != null)
            {
                var inte = new IntegridadDatos();
                var aud = new Auditoria();

                var BitacoraDVH = inte.CalcularDVH(idioma.Descripcion + "MODIFICAR IDIOMA" + "INFO");

                aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR IDIOMA", "INFO", "Se actualizó el idioma: " + idioma.Id + " - '" + descripcionAnterior + "' a '" + idioma.Descripcion + "'", BitacoraDVH);
            }

        }

        public void BorrarPorId(Idioma idioma, string usuario)
        {
            var ad = new IdiomaDAC();
            
                        
            var inte = new IntegridadDatos();

            var aud = new Auditoria();

            ad.BorrarPorId(idioma.Id);

            var BitacoraDVH = inte.CalcularDVH(idioma.Id.ToString() + "BORRAR IDIOMA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR IDIOMA", "INFO", "Se borró el idioma: " + idioma.Id + " - " + idioma.Descripcion, BitacoraDVH);

        }

        public Idioma BuscarPorId(int id)
        {
            var ad = new IdiomaDAC();

            return (ad.BuscarPorId(id));

        }

        public IEnumerable<Idioma> Listar()
        {
            var ad = new IdiomaDAC();

            return (ad.Listar());

        }


    }
}