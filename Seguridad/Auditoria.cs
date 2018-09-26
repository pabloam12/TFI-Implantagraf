using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad
{
    public class Auditoria
    {
        public List<Bitacora> ConsultarBitacora()
        {
            var ad = new BitacoraDAC();

            return (ad.ConsultarBitacora());

        }

        public List<Bitacora> ConsultarBitacora(string fecha, string fechaFin, string usr, string accion, string criticidad)
        {
            var ad = new BitacoraDAC();

            return (ad.ConsultarBitacora(fecha, fechaFin, usr, accion, criticidad));

        }

        public void grabarBitacora(DateTime fechaHora, String usuario, String descripcion, String criticidad, String detalle, long DVH)
        {
            var ad = new BitacoraDAC();
            var integ = new IntegridadDatos();

            ad.grabarBitacora(fechaHora, usuario, descripcion, criticidad, detalle, DVH);

            integ.RecalcularDVV("SEG_Bitacora");

        }
    }
}