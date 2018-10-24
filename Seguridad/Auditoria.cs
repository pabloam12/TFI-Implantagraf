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

        public void grabarBitacora(DateTime fechaHora, String usuario, String accion, String criticidad, String detalle)
        {
            var ad = new BitacoraDAC();
            var integ = new IntegridadDatos();

            var BitacoraDVH = integ.CalcularDVH(fechaHora.ToString() + usuario + accion + criticidad + detalle);

            ad.grabarBitacora(fechaHora, usuario, accion, criticidad, detalle, BitacoraDVH);
                        
            integ.RecalcularDVV("SEG_Bitacora");

        }
    }
}