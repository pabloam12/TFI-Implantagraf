using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class WebService
    {

        public int CalcularInteres(int importeTotal, int numCuotas)
        {

            if (numCuotas == 12)
            { return ((Int32)(importeTotal * 1.70) / 12); }

            if (numCuotas == 6)
            { return ((Int32)(importeTotal * 1.50) / 6); }

            if (numCuotas == 3)
            { return ((Int32)(importeTotal * 1.25) / 3); }

            return importeTotal;
        }


        public long ValidarTarjeta(string numero, int marcaId, string mesVenc, string anioVenc, string codSeguridad)
        {
            var accDatos = new WS_DAC();

            return accDatos.ValidarTarjeta(numero, marcaId, mesVenc, anioVenc, codSeguridad);
        }

        public bool ValidarCUIT(string cuit)
        {

            bool resultado = false;

            int Verificador = EncontrarVerificador(cuit);
            resultado = (cuit[10].ToString() == Verificador.ToString());
            
            return resultado;
        }

        private int EncontrarVerificador(string CUIT)
        {
            int Sumador = 0;
            int Producto = 0;
            int Coeficiente = 0;
            int Resta = 5;
            for (int i = 0; i < 10; i++)
            {
                if (i == 4) Resta = 11;
                Producto = CUIT[i];
                Producto -= 48;
                Coeficiente = Resta - i;
                Producto = Producto * Coeficiente;
                Sumador = Sumador + Producto;
            }

            int Resultado = Sumador - (11 * (Sumador / 11));
            Resultado = 11 - Resultado;

            if (Resultado == 11) return 0;
            else return Resultado;
        }
    }
}