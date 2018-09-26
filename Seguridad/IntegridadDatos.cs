using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Seguridad
{
    public class IntegridadDatos
    {
        public List<string> ListarTablasFaltantes()
        {
            string[] tablasImplantagraf = { "Carrito", "Categoria", "Marca", "Cliente", "DetalleOperacion", "Factura", "FormaPago", "ItemsCarrito", "Localidad", "Operacion", "Producto", "SEG_DVV", "Stock", "TipoOperacion", "SEG_Bitacora", "SEG_PerfilUsr", "SEG_Usuario", "Idioma", "SEG_EstadosCuenta" };

            List<String> tablasFaltantes = new List<String>();

            for (int i = 0; i < tablasImplantagraf.Length; i++)
            {
                string tabla = tablasImplantagraf[i];

                if (ValidarExistencia(tabla) == 0)

                { tablasFaltantes.Add(tabla); }

            }

            return tablasFaltantes;
        }

        public bool ValidarIntegridadTablas()
        {
            string[] tablasImplantagraf = { "Carrito", "Categoria", "Marca", "Cliente", "DetalleOperacion", "Factura", "FormaPago", "ItemsCarrito", "Localidad", "Operacion", "Producto", "SEG_DVV", "Stock", "TipoOperacion", "SEG_Bitacora", "SEG_PerfilUsr", "SEG_Usuario", "Idioma", "SEG_EstadosCuenta" };

            for (int i = 0; i < tablasImplantagraf.Length; i++)
            {
                string tabla = tablasImplantagraf[i];

                //Retorna true cuando falla la integridad.
                if (ValidarExistencia(tabla) == 0)
                { return true; }

            }

            return false;
        }

        public bool ValidarTablasDVV()
        {
            string[] tablasDVV = { "SEG_Usuario", "SEG_Bitacora", "Operacion", "Cliente", "Factura" };
            long DVV = 0;

            for (int i = 0; i < tablasDVV.Length; i++)
            {
                string tabla = tablasDVV[i];

                if (ContarRegistros(tabla) != 0)
                {
                    DVV = CalcularDVV(tabla);
                    
                    // Retorna true cuando el valor no coincide.
                    if (ValidarDVV(tabla, DVV) == 0)
                    { return true; }
                }
                                                
            }

            return false;
        }


        public void RecalcularDVV()
        {
            string[] tablasDVV = { "SEG_Usuario", "SEG_Bitacora", "Operacion", "Cliente", "Factura" };
            long DVV = 0;

            for (int i = 0; i < tablasDVV.Length; i++)
            {
                string tabla = tablasDVV[i];

                DVV = CalcularDVV(tabla);

                ActualizarDVV(tabla, DVV);
            }

        }

        public void RecalcularDVV(string tabla)
        {
            long DVV = 0;

            DVV = CalcularDVV(tabla);
            ActualizarDVV(tabla, DVV);

        }

        private long CalcularDVV(string tabla)
        {
            var accDatos = new IntegridadDAC();

            return accDatos.CalcularDVV(tabla);
        }

        private int ValidarDVV(string tabla, long DVV)
        {
            var accDatos = new IntegridadDAC();

            return accDatos.ValidarDVV(tabla, DVV);
        }


        private void ActualizarDVV(string tabla, long DVV)
        {
            var accDatos = new IntegridadDAC();

            accDatos.ActualizarDVV(tabla, DVV);
        }

        public int ContarRegistros(string tabla)
        {
            var accDatos = new IntegridadDAC();

            return accDatos.ContarRegistros(tabla);
        }

        private int ValidarExistencia(string tabla)
        {
            var accDatos = new IntegridadDAC();

            return accDatos.ValidarExistencia(tabla);

        }

        public long CalcularDVH(string cadena)
        {
            var DVH = 0;
            var I = 1;

            if (cadena == null || cadena == "")
            {
                return DVH;
            }

            for (I = 1; I <= cadena.Length; I++)
            {
                DVH = DVH + Encoding.ASCII.GetBytes(cadena.Substring(I - 1, 1))[0];
            }

            return DVH;
        }
    }
}