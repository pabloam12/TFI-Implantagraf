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

            var localidadActual = ad.Agregar(localidad);

            var inte = new IntegridadDatos();

            var localidadActualDVH = inte.CalcularDVH(localidadActual.Id.ToString() + localidadActual.Descripcion);

            inte.ActualizarDVHLocalidad(localidadActual.Id, localidadActualDVH);
            inte.RecalcularDVV("Localidad");

            var aud = new Auditoria();

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA LOCALIDAD", "INFO", "Se creó la localidad: " + localidad.Id + " - '" + localidad.Descripcion + "'");

            return (localidadActual);

        }

        public void ActualizarPorId(Localidad localidad, string usuario)
        {
            var ad = new LocalidadDAC();

            var aud = new Auditoria();

            var descripcionAnterior = BuscarPorId(localidad.Id).Descripcion;

            ad.ActualizarPorId(localidad);

            var inte = new IntegridadDatos();

            var localidadActualDVH = inte.CalcularDVH(localidad.Id.ToString() + localidad.Descripcion);

            inte.ActualizarDVHLocalidad(localidad.Id, localidadActualDVH);
            inte.RecalcularDVV("Localidad");

            aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR LOCALIDA", "INFO", "Se actualizó la localidad: " + localidad.Id + " - '" + descripcionAnterior + "' a '" + localidad.Descripcion + "'");


        }

        public void BorrarPorId(Localidad localidad, string usuario)
        {
            var ad = new LocalidadDAC();

            ad.BorrarPorId(localidad.Id);
            
            var aud = new Auditoria();
                        
            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR LOCALIDAD", "INFO", "Se borró la localidad: " + localidad.Id + " - '" + localidad.Descripcion + "'");

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