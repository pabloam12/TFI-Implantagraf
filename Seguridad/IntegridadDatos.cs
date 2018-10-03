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
        public bool ValidarIntegridadGlobal()
        {
            var flag = false;

            if (ValidarRegistrosDVH())
                flag = true;

            if (ValidarTablasDVV(flag))
                flag = true;

            if (ValidarIntegridadTablas())
                flag = true;

            return flag;
        }

        public bool ValidarIntegridadTablas()
        {
            string[] tablasImplantagraf = { "Carrito", "Categoria", "Marca", "Cliente", "DetalleOperacion", "Factura", "FormaPago", "ItemsCarrito", "Localidad", "Operacion", "Producto", "SEG_DVV", "Stock", "TipoOperacion", "SEG_Bitacora", "SEG_PerfilUsr", "SEG_Usuario", "Idioma", "SEG_EstadosCuenta" };

            var flag = false;

            for (int i = 0; i < tablasImplantagraf.Length; i++)
            {
                string tabla = tablasImplantagraf[i];

                if (ValidarExistencia(tabla) == 0)

                {
                    grabarRegistroIntegridad("SE ELIMINÓ", "Tabla: " + tabla);
                    flag = true;
                }

            }

            //Retorna true cuando falla la integridad.
            return flag;
        }

        public bool ValidarTablasDVV(bool flag)
        {
            string[] tablasDVV = { "SEG_Usuario", "SEG_Bitacora", "Operacion", "Cliente", "Factura" };
            long DVV = 0;

            for (int i = 0; i < tablasDVV.Length; i++)
            {
                string tabla = tablasDVV[i];
                var cant = 0;

                if (ValidarExistencia(tabla) != 0)
                {
                    if ((cant = ContarRegistros(tabla)) != 0)
                    {
                        DVV = CalcularDVV(tabla);

                        // Retorna true cuando el valor no coincide el valor DVV.
                        if (ValidarDVV(tabla, DVV) == 0)
                        {
                            //NoCoincide el valor DVV ni la cantidad de registros.
                            if (ValidarCantidadReg(tabla, cant) == 0)
                            {
                                grabarRegistroIntegridad("SE ALTERÓ EL Nº DE REGISTROS", "Tabla: " + tabla);
                            }

                            return true;
                        }
                    }
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

                var CantReg = ContarRegistros(tabla);

                ActualizarDVV(tabla, DVV, CantReg);
            }

        }

        public void RecalcularDVV(string tabla)
        {
            long DVV = 0;

            DVV = CalcularDVV(tabla);

            var cantReg = ContarRegistros(tabla);

            ActualizarDVV(tabla, DVV, cantReg);

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

        private int ValidarCantidadReg(string tabla, int cantReg)
        {
            var accDatos = new IntegridadDAC();

            return accDatos.ValidarDVV(tabla, cantReg);
        }

        private void ActualizarDVV(string tabla, long DVV, int CantReg)
        {
            var accDatos = new IntegridadDAC();

            accDatos.ActualizarDVV(tabla, DVV, CantReg);
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

        public void grabarRegistroIntegridad(string Col_A = "N/A", string Col_B = "N/A", string Col_C = "N/A", string Col_D = "N/A", string Col_E = "N/A", string Col_F = "N/A", string Col_G = "N/A")
        {
            var accDatos = new IntegridadDAC();

            accDatos.grabarRegistroIntegridad(Col_A, Col_B, Col_C, Col_D, Col_E, Col_F, Col_G);

        }

        public List<IntegridadRegistros> ListarRegistrosTablasFaltantes()
        {
            var accDatos = new IntegridadDAC();

            return accDatos.ListarRegistrosTablasFaltantes();

        }

        public void LimpiarTablaRegistrosTablasFaltantes()
        {
            var accDatos = new IntegridadDAC();

            accDatos.LimpiarTablaRegistrosTablasFaltantes();

        }

        public bool ValidarRegistrosDVH()
        {
            var flag = false;
            var accDatosUsuario = new CuentaDAC();

            List<Usuario> listadoUsuarios = new List<Usuario>();

            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(1);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                if (CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw) != usuarioActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: SEG_Usuario", "Código: " + usuarioActual.Id.ToString(), "Razon Social: " + usuarioActual.RazonSocial, "CUIL: " + usuarioActual.CUIL, "Perfil: " + usuarioActual.PerfilUsr.Descripcion, "Usuario: " + usuarioActual.Usr);
                    flag = true;
                }
            }

            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(2);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                if (CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw) != usuarioActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: SEG_Usuario", "Código: " + usuarioActual.Id.ToString(), "Razon Social: " + usuarioActual.RazonSocial, "CUIL: " + usuarioActual.CUIL, "Perfil: " + usuarioActual.PerfilUsr.Descripcion, "Usuario: " + usuarioActual.Usr);
                    flag = true;
                }
            }

            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(3);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                if (CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw) != usuarioActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: SEG_Usuario", "Código: " + usuarioActual.Id.ToString(), "Razon Social: " + usuarioActual.RazonSocial, "CUIL: " + usuarioActual.CUIL, "Perfil: " + usuarioActual.PerfilUsr.Descripcion, "Usuario: " + usuarioActual.Usr);
                    flag = true;
                }
            }

            //AGREGAR EL RESTO DE LOS FOREACH.

            //accDatos.ValidarBitacoraDVH();
            //accDatos.ValidarOperacionDVH();
            //accDatos.ValidarClienteDVH();
            //accDatos.ValidarFacturaDVH();

            return flag;
        }

        public void ActualizarDVHUsuario(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHUsuario(id, DVH);
        }

        public void RecalcularTodosDVH()
        {
            //TODO FALTAN EL RESTO DE LAS TABLAS.

            var accDatosUsuario = new CuentaDAC();
            long dvhActual;

            List<Usuario> listadoUsuarios = new List<Usuario>();

            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(1);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                dvhActual = CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw);

                ActualizarDVHUsuario(usuarioActual.Id, dvhActual);
            }

            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(2);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                dvhActual = CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw);

                ActualizarDVHUsuario(usuarioActual.Id, dvhActual);
            }

            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(3);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                dvhActual = CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw);

                ActualizarDVHUsuario(usuarioActual.Id, dvhActual);
            }

            RecalcularDVV("SEG_Usuario");
        }

        public void RealizarBackUp()
        {

            var integridad = new IntegridadDAC();

            integridad.RealizarBackUp();

        }

        public IEnumerable<Respaldo> ListarRespaldos()
        {
            var integridad = new IntegridadDAC();

            return integridad.ListarRespaldos();

        }

        public void RestaurarCopiaRespaldo(string rutaCompleta)
        {
            var integridad = new IntegridadDAC();

            integridad.RestaurarCopiaRespaldo(rutaCompleta);

        }
    }
}