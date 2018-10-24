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

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA IDIOMA", "INFO", "Se creó el idioma: " + idioma.Id + " - " + idioma.Descripcion);

            return (idioma);

        }

        public void ActualizarPorId(Idioma idioma, string usuario)
        {
            var ad = new IdiomaDAC();

            var descripcionAnterior = BuscarPorId(idioma.Id).Descripcion;

            ad.ActualizarPorId(idioma);

            if (idioma.Descripcion != null)
            {
                var aud = new Auditoria();

                aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR IDIOMA", "INFO", "Se actualizó el idioma: " + idioma.Id + " - '" + descripcionAnterior + "' a '" + idioma.Descripcion + "'");
            }

        }

        public void BorrarPorId(Idioma idioma, string usuario)
        {
            var ad = new IdiomaDAC();
            
            var aud = new Auditoria();

            ad.BorrarPorId(idioma.Id);

            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR IDIOMA", "INFO", "Se borró el idioma: " + idioma.Id + " - " + idioma.Descripcion);

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