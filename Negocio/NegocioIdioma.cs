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
            var integ = new IntegridadDatos();
            var aud = new Auditoria();

            var DVH = integ.CalcularDVH(idioma.Id.ToString() + idioma.Descripcion + idioma.Abreviacion);

            idioma = ad.Agregar(idioma, DVH);

            integ.RecalcularDVV("SEG_Idioma");

            var BitacoraDVH = integ.CalcularDVH(idioma.Id.ToString() + idioma.Descripcion + idioma.Abreviacion + "MODIFICAR IDIOMA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA IDIOMA", "INFO", "Se creó el idioma: " + idioma.Id + " - " + idioma.Descripcion, DVH);

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

                var BitacoraDVH = inte.CalcularDVH(idioma.Id.ToString()+idioma.Descripcion+idioma.Abreviacion +  "MODIFICAR IDIOMA" + "INFO");

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