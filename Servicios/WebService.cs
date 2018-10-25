using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class WebService
    {

        public decimal CalcularInteres(int importeTotal, int numCuotas)
        {
            return 0;
        }


        public long ValidarTarjeta(string numero, int marcaId, string mesVenc, string anioVenc, string codSeguridad)
        {
            var accDatos = new WS_DAC();

            return accDatos.ValidarTarjeta(numero, marcaId, mesVenc, anioVenc, codSeguridad);
        }

        public bool ValidarLimite(int importeTotal)
        {
            return false;
        }
    }
}