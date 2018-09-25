using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioLocalidad
    {
        public Localidad Agregar(Localidad localidad, string usuario)
        {
            var ad = new LocalidadDAC();

            localidad = ad.Agregar(localidad);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "ALTA LOCALIDAD" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA LOCALIDAD", "INFO", "Se creó la localidad: " + localidad.Id + " - '" + localidad.Descripcion + "'", BitacoraDVH);

            return (localidad);

        }

        public void ActualizarPorId(Localidad localidad, string usuario)
        {
            var ad = new LocalidadDAC();

            var descripcionAnterior = BuscarPorId(localidad.Id).Descripcion;

            ad.ActualizarPorId(localidad);

            if (localidad.Descripcion != null)
            {
                var inte = new IntegridadDatos();
                var aud = new Auditoria();

                var BitacoraDVH = inte.CalcularDVH(usuario + "ACTUALIZAR LOCALIDAD" + "INFO");

                aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR LOCALIDAD", "INFO", "Se actualizó la localidad: " + localidad.Id + " - '" + descripcionAnterior + "' a '" + localidad.Descripcion + "'", BitacoraDVH);
            }

        }

        public void BorrarPorId(Localidad localidad, string usuario)
        {
            var ad = new LocalidadDAC();

            ad.BorrarPorId(localidad.Id);

            var inte = new IntegridadDatos();

            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "BORRAR LOCALIDAD" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR LOCALIDAD", "INFO", "Se borró la localidad: " + localidad.Id + " - '" + localidad.Descripcion + "'", BitacoraDVH);

        }

        public Localidad BuscarPorId(int id)
        {
            var ad = new LocalidadDAC();

            return ad.BuscarPorId(id);

        }

        public IEnumerable<Localidad> Listar()
        {
            var ad = new LocalidadDAC();

            return (ad.Listar());

        }


    }
}