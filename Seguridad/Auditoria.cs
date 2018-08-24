using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seguridad
{
    public class Auditoria
    {

        public bool grabarBitacora(DateTime fechaHora, String usuario, String descripcion, String criticidad, long DVH)
        {
            var ad = new BitacoraDAC();

            return (ad.grabarBitacora(fechaHora, usuario, descripcion, criticidad, DVH));

        }
    }
}