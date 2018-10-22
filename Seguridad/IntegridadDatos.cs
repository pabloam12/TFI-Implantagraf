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
            string[] tablasImplantagraf = { "Categoria", "Marca", "Cliente", "DetalleOperacion",
                                            "Factura", "FormaPago", "Idioma", "Localidad", "Marca", "Operacion",
                                            "Producto", "SEG_Bitacora", "SEG_DetallePermisos", "SEG_DVV", "SEG_EstadosCuenta",
                                            "SEG_IntegridadRegistros", "SEG_PerfilUsr", "SEG_Permisos", "SEG_Usuario", "Stock",
                                            "WS_Empresa_TC" };

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
            string[] tablasDVV = { "Categoria", "Marca", "Cliente", "DetalleOperacion",
                                   "Factura", "FormaPago", "Idioma", "Localidad", "Marca", "Operacion",
                                   "Producto", "SEG_Bitacora", "SEG_DetallePermisos", "SEG_EstadosCuenta",
                                   "SEG_IntegridadRegistros", "SEG_PerfilUsr", "SEG_Permisos", "SEG_Usuario", "Stock",
                                   "WS_Empresa_TC" };
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
            string[] tablasDVV = { "Categoria", "Marca", "Cliente", "DetalleOperacion",
                                   "Factura", "FormaPago", "Idioma", "Localidad", "Marca", "Operacion",
                                   "Producto", "SEG_Bitacora", "SEG_DetallePermisos", "SEG_EstadosCuenta",
                                   "SEG_IntegridadRegistros", "SEG_PerfilUsr", "SEG_Permisos", "SEG_Usuario", "Stock",
                                   "WS_Empresa_TC" };
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
            var accDatosFormaPago = new FormaPagoDAC();
            var accDatosIdioma = new IdiomaDAC();
            var accDatosLocalidad = new LocalidadDAC();
            var accDatosMarca = new MarcaDAC();
            var accDatosBitacora = new BitacoraDAC();

            List<Usuario> listadoUsuarios = new List<Usuario>();
            List<Producto> listadoProductos = new List<Producto>();
            List<Categoria> listadoCategorias = new List<Categoria>();
            List<Cliente> listadoClientes = new List<Cliente>();
            List<DetalleOperacion> listadoDetalleOperaciones = new List<DetalleOperacion>();
            List<Factura> listadoFacturas = new List<Factura>();
            List<FormaPago> listadoFormasPago = new List<FormaPago>();
            List<Idioma> listadoIdiomas = new List<Idioma>();
            List<Localidad> listadoLocalidades = new List<Localidad>();
            List<Marca> listadoMarcas = new List<Marca>();
            List<Operacion> listadoOperaciones = new List<Operacion>();
            List<Bitacora> listadoBitacora = new List<Bitacora>();

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
            listadoDetalleOperaciones = accDatosOperaciones.ListarDetalleOperacion();

            foreach (DetalleOperacion detalleActual in listadoDetalleOperaciones)
            {
                if (CalcularDVH(detalleActual.OperacionId.ToString() + detalleActual.ProductoId.ToString() + detalleActual.SubTotal.ToString()) != detalleActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: DetalleOperacion", "Operación: " + detalleActual.OperacionId.ToString(), "Producto: " + detalleActual.ProductoId.ToString(), "SubTotal: " + detalleActual.SubTotal.ToString());
                    flag = true;
                }
            }

            // Facturas
            listadoFacturas = accDatosOperaciones.ListarFacturas();

            foreach (Factura facturaActual in listadoFacturas)
            {
                if (CalcularDVH(facturaActual.Codigo.ToString() + facturaActual.RazonSocial + facturaActual.Monto.ToString()) != facturaActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Factura", "Código: " + facturaActual.Codigo.ToString(), "Razon Social: " + facturaActual.RazonSocial, "Monto: " + facturaActual.Monto.ToString());
                    flag = true;
                }
            }

            // Formas de Pago
            listadoFormasPago = accDatosFormaPago.Listar();

            foreach (FormaPago formaPagoActual in listadoFormasPago)
            {
                if (CalcularDVH(formaPagoActual.Id.ToString() + formaPagoActual.Descripcion) != formaPagoActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: FormaPago", "Código: " + formaPagoActual.Id.ToString(), "Descripción: " + formaPagoActual.Descripcion);
                    flag = true;
                }
            }

            // Idioma
            listadoIdiomas = accDatosIdioma.Listar();

            foreach (Idioma idiomaActual in listadoIdiomas)
            {
                if (CalcularDVH(idiomaActual.Id.ToString() + idiomaActual.Descripcion + idiomaActual.Abreviacion) != idiomaActual.DVH)
                { 
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Idioma", "Código: " + idiomaActual.Id.ToString(), "Descripción: " + idiomaActual.Descripcion);
                    flag = true;
                }
            }

            // Localidad
            listadoLocalidades = accDatosLocalidad.Listar();

            foreach (Localidad localidadActual in listadoLocalidades)
            {
                if (CalcularDVH(localidadActual.Id.ToString() + localidadActual.Descripcion) != localidadActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Localidad", "Código: " + localidadActual.Id.ToString(), "Descripción: " + localidadActual.Descripcion);
                    flag = true;
                }
            }

            // Marca
            listadoMarcas = accDatosMarca.Listar();

            foreach (Marca marcaActual in listadoMarcas)
            {
                if (CalcularDVH(marcaActual.Id.ToString() + marcaActual.Descripcion) != marcaActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Marca", "Código: " + marcaActual.Id.ToString(), "Descripción: " + marcaActual.Descripcion);
                    flag = true;
                }
            }

            // Operaciones
            listadoOperaciones = accDatosOperaciones.ListarOperaciones();

            foreach (Operacion operacionActual in listadoOperaciones)
            {
                if (CalcularDVH(operacionActual.Id.ToString() + operacionActual.ClienteId.ToString() + operacionActual.FechaHora.ToString()+ operacionActual.TipoOperacion+operacionActual.ImporteTotal.ToString()+operacionActual.FacturaId.ToString()) != operacionActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Operacion", "Código: " + operacionActual.Id.ToString(), "Cliente: " + operacionActual.ClienteId.ToString(), "Fecha/Hora: " + operacionActual.FechaHora.ToString(), "Importe Total: " + operacionActual.ImporteTotal.ToString(), "Factura: "+ operacionActual.FacturaId.ToString());
                    flag = true;
                }
            }

            // Bitácora
            listadoBitacora = accDatosBitacora.ConsultarBitacora();

            foreach (Bitacora bitacoraActual in listadoBitacora)
            {
                if (CalcularDVH(bitacoraActual.Id.ToString() + bitacoraActual.Id.ToString() + bitacoraActual.FechaHora.ToString() + bitacoraActual.TipoOperacion + bitacoraActual.ImporteTotal.ToString() + bitacoraActual.FacturaId.ToString()) != bitacoraActual.DVH)
                {
                    grabarRegistroIntegridad("SE MODIFICÓ REGISTRO", "Tabla: Operacion", "Código: " + bitacoraActual.Id.ToString(), "Cliente: " + bitacoraActual.ClienteId.ToString(), "Fecha/Hora: " + bitacoraActual.FechaHora.ToString(), "Importe Total: " + bitacoraActual.ImporteTotal.ToString(), "Factura: " + bitacoraActual.FacturaId.ToString());
                    flag = true;
                }
            }

            //Retorno el resultado final.
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