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

            //if (ValidarRegistrosDVH())
            //    flag = true;

            //if (ValidarTablasDVV(flag))
            //    flag = true;

            //if (ValidarIntegridadTablas())
            //    flag = true;

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
            var accDatosProductos = new ProductoDAC();
            var accDatosCategorias = new CategoriaDAC();
            var accDatosClientes = new ClienteDAC();
            var accDatosOperaciones = new OperacionesDAC();

            List<Usuario> listadoUsuarios = new List<Usuario>();
            List<Producto> listadoProductos = new List<Producto>();
            List<Categoria> listadoCategorias = new List<Categoria>();
            List<Cliente> listadoClientes = new List<Cliente>();

            // Usuarios.
            listadoUsuarios = accDatosUsuario.ListarUsuarios();

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                if (CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw) != usuarioActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: SEG_Usuario", "Código: " + usuarioActual.Id.ToString(), "Razón Social: " + usuarioActual.RazonSocial, "CUIL: " + usuarioActual.CUIL, "Perfil: " + usuarioActual.PerfilUsr.Descripcion, "Usuario: " + usuarioActual.Usr);
                    flag = true;
                }
            }

           
            // Productos.
            listadoProductos = accDatosProductos.ListarProductos();

            foreach (Producto productoActual in listadoProductos)
            {
                if(CalcularDVH(productoActual.Codigo + productoActual.Titulo + productoActual.Modelo + productoActual.Imagen + productoActual.Marca.Id.ToString() + productoActual.Marca.Id.ToString() + productoActual.Precio.ToString()) != productoActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Producto", "Código: " + productoActual.Codigo.ToString(), "Título: " + productoActual.Titulo, "Modelo: " + productoActual.Modelo, "Marca: " + productoActual.Marca.Descripcion, "Categoría: " + productoActual.Categoria.Descripcion);
                    flag = true;
                }
            }


            // Categorias.
            listadoCategorias = accDatosCategorias.Listar();

            foreach (Categoria categoriaActual in listadoCategorias)
            {
                if (CalcularDVH(categoriaActual.Id.ToString() + categoriaActual.Descripcion) != categoriaActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Categoría", "Código: " + categoriaActual.Id.ToString(), "Descripción: " + categoriaActual.Descripcion);
                    flag = true;
                }
            }

            // Clientes
            listadoClientes = accDatosClientes.Listar();

            foreach (Cliente clienteActual in listadoClientes)
            {
                if (CalcularDVH(clienteActual.RazonSocial + clienteActual.CUIL + clienteActual.Email + clienteActual.Telefono + clienteActual.Direccion + clienteActual.FechaAlta.ToString()) != clienteActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Cliente", "Código: " + clienteActual.Id.ToString(), "Razón Social: " + clienteActual.RazonSocial, "CUIL: " + clienteActual.CUIL, "Email: " + clienteActual.Email, "Fecha Alta: " + clienteActual.FechaAlta.ToString());
                    flag = true;
                }
            }

            // Detalle Operacion
            //listadoDetalleOperaciones = accDatosOperaciones.Listar();

            foreach (Cliente clienteActual in listadoClientes)
            {
                if (CalcularDVH(clienteActual.RazonSocial + clienteActual.CUIL + clienteActual.Email + clienteActual.Telefono + clienteActual.Direccion + clienteActual.FechaAlta.ToString()) != clienteActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Cliente", "Código: " + clienteActual.Id.ToString(), "Razón Social: " + clienteActual.RazonSocial, "CUIL: " + clienteActual.CUIL, "Email: " + clienteActual.Email, "Fecha Alta: " + clienteActual.FechaAlta.ToString());
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

        public void ActualizarDVHProducto(int cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHProducto(cod, DVH);
        }

        public void RecalcularTodosDVH()
        {
            //TODO FALTAN EL RESTO DE LAS TABLAS.

            var accDatosUsuario = new CuentaDAC();
            var accDatosProductos = new ProductoDAC();

            long dvhActual;

            List<Usuario> listadoUsuarios = new List<Usuario>();
            List<Producto> listadoProductos = new List<Producto>();

            // Usuarios Webmasters.
            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(1);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                dvhActual = CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw);

                ActualizarDVHUsuario(usuarioActual.Id, dvhActual);
            }

            // Usuarios Administrativos.
            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(2);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                dvhActual = CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw);

                ActualizarDVHUsuario(usuarioActual.Id, dvhActual);
            }

            // Usuarios Clientes.
            listadoUsuarios = accDatosUsuario.ListarUsuariosPorPerfil(3);

            foreach (Usuario usuarioActual in listadoUsuarios)
            {
                dvhActual = CalcularDVH(usuarioActual.RazonSocial + usuarioActual.CUIL + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Usr + usuarioActual.Psw);

                ActualizarDVHUsuario(usuarioActual.Id, dvhActual);
            }

            // Productos.
            listadoProductos = accDatosProductos.ListarProductos();

            foreach (Producto productoActual in listadoProductos)
            {
                dvhActual = CalcularDVH(productoActual.Codigo + productoActual.Titulo + productoActual.Modelo + productoActual.Imagen + productoActual.Marca.Id.ToString() + productoActual.Marca.Id.ToString()+ productoActual.Precio.ToString());

                ActualizarDVHProducto(productoActual.Codigo, dvhActual);
            }

            RecalcularDVV("SEG_Usuario");
            RecalcularDVV("Producto");
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