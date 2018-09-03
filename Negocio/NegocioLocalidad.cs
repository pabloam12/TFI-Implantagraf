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
        public Localidad Agregar(Localidad localidad)
        {
            var ad = new LocalidadDAC();

            var nuevaLocalidad = ad.Agregar(localidad);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var DVHBitacora = inte.CalcularDVH(localidad.Descripcion + "ALTA LOCALIDAD" + "INFO");

            aud.grabarBitacora(DateTime.Now, localidad.Descripcion, "ALTA LOCALIDAD", "INFO", DVHBitacora);

            return (nuevaLocalidad);

        }

        public void ActualizarPorId(Localidad localidad)
        {
            var ad = new LocalidadDAC();

            ad.ActualizarPorId(localidad);

            if (localidad.Descripcion != null)
            {
                var inte = new IntegridadDatos();
                var aud = new Auditoria();

                var DVHBitacora = inte.CalcularDVH(localidad.Descripcion + "MODIFICACION LOCALIDAD" + "INFO");

                aud.grabarBitacora(DateTime.Now, localidad.Id + "-" + localidad.Descripcion, "MODIFICACION LOCALIDAD", "INFO", DVHBitacora);
            }

        }

        public void BorrarPorId(int id)
        {
            var ad = new LocalidadDAC();

            ad.BorrarPorId(id);

            var inte = new IntegridadDatos();

            var aud = new Auditoria();

            var DVHBitacora = inte.CalcularDVH(id.ToString() + "ELIMINO LOCALIDAD" + "INFO");

            aud.grabarBitacora(DateTime.Now, id.ToString(), "ELIMINO LOCALIDAD", "INFO", DVHBitacora);

        }

        public Localidad ListarPorId(int id)
        {
            var ad = new LocalidadDAC();

            return ad.ListarPorId(id);

        }

        public List<Localidad> Listar()
        {
            var ad = new LocalidadDAC();

            return (ad.Listar());

        }


    }
}