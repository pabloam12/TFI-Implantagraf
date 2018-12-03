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
            var accDatosUSr = new CuentaDAC();

            var flag = false;


            if (ValidarExistenciaBase() == 0)
            {
                CrearBaseImplantagraf();

                RestaurarCopiaRespaldo("C:\\Implantagraf\\RESPALDOS\\Implantagraf_Inicial.bak");

                GrabarRegistroIntegridad("SE ELIMINÓ BD", "TODAS");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR BASE DATOS", "ERROR GRAVE", "Error al intentar encontrar la BD.");
                
                flag = true;
            }


            if (ValidarExistencia("SEG_DVV") == 0)
            {
                GrabarRegistroIntegridad("SE ELIMINÓ", "SEG_DVV");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR TABLA", "ERROR GRAVE", "Error al intentar encontrar la tabla SEG_DVV.");

                CreoTablaDVV();

                flag = true;

            }



            //Hace una validacion de integridad de ciertas tablas core.

            //Idioma
            if (ValidarExistencia("Idioma") == 0)
            {
                GrabarRegistroIntegridad("SE ELIMINÓ", "IDIOMA");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR TABLA", "ERROR GRAVE", "Error al intentar encontrar la tabla IDIOMA.");


                CreoTablaIdioma();

                flag = true;

            }


            flag = ValidaSoloIdioma(flag);


            //Localidad
            if (ValidarExistencia("Localidad") == 0)
            {
                GrabarRegistroIntegridad("SE ELIMINÓ", "LOCALIDAD");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR TABLA", "ERROR GRAVE", "Error al intentar encontrar la tabla LOCALIDAD.");


                CreoTablaLocalidad();

                flag = true;

            }

            flag = ValidaSoloLocalidad(flag);



            //PerfilUsr
            if (ValidarExistencia("SEG_PerfilUsr") == 0)
            {
                GrabarRegistroIntegridad("SE ELIMINÓ", "SEG_PERFILUSR");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR TABLA", "ERROR GRAVE", "Error al intentar encontrar la tabla SEG_PERFILUSR.");


                CreoTablaPerfilUsr();

                flag = true;

            }

            flag = ValidaSoloPerfilUsr(flag);

            //Permisos
            if (ValidarExistencia("SEG_Permisos") == 0)
            {
                GrabarRegistroIntegridad("SE ELIMINÓ", "SEG_PERMISOS");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR TABLA", "ERROR GRAVE", "Error al intentar encontrar la tabla SEG_PERMISOS.");

                CreoTablaPermisosUsr();

                flag = true;

            }

            flag = ValidaSoloPermisosUsr(flag);


            // Valido por integridad la tabla de permisos
            if (ValidarExistencia("SEG_DetallePermisos") == 0)
            {
                GrabarRegistroIntegridad("SE ELIMINÓ", "SEG_DETALLEPERMISOS");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR TABLA", "ERROR GRAVE", "Error al intentar encontrar la tabla SEG_DETALLEPERMISOS.");


                CreoTablaDetallePermisosUsr();

                flag = true;
            }


            // Valida que esten todas las tablas.
            if (ValidarExistenciaTablas(flag))
                flag = true;

            // Valida que no se haya alterado el numero de registros de cada tabla.
            if (ValidarTablasDVV(flag))
                flag = true;

            //Inserto por Integridad Tablas Core.
            InsertarIdiomaCompleto();
            //InsertarLocalidadCompleto();
            InsertarPerfilUsrCompleto();
            InsertarPermisosCompleto();


            //Usuario
            if (ValidarExistencia("SEG_Usuario") == 0)
            {
                GrabarRegistroIntegridad("SE ELIMINÓ", "SEG_USUARIO");

                var audi = new Auditoria();
                audi.grabarBitacora(DateTime.Now, "SISTEMA", "ERROR TABLA", "ERROR GRAVE", "Error al intentar encontrar la tabla SEG_USUARIO.");


                CreoTablaUsuario();

                flag = true;
            }


            if (accDatosUSr.ValidarUsuario("admin") == true)
            {
                var usrAdmin = AltaUsuario();

                accDatosUSr.OtorgarPermisosWebmaster(usrAdmin.Id);

                GrabarRegistroIntegridad("SE ELIMINÓ", "USUARIO ADMIN");

                flag = true;
            }


            // Valida que no se haya alterado ningún registro.

            if (ValidarRegistrosDVH(flag))
                flag = true;


            return flag;
        }

        private int ValidarExistenciaBase()
        {
            var integ = new IntegridadDAC();

            return integ.ValidarExistenciaBase();

        }

        private void CrearBaseImplantagraf()
        {
            var integ = new IntegridadDAC();

            integ.CrearBaseImplantagraf();

        }

        private Usuario AltaUsuario()
        {
            var priv = new Privacidad();
            var usrAdmin = new Usuario();
            var accDatosUSr = new CuentaDAC();


            usrAdmin.Nombre = "WebMaster";
            usrAdmin.Apellido = "WebMaster";
            usrAdmin.Email = "pablo.a.mahiques@gmail.com";
            usrAdmin.Usr = "admin";
            usrAdmin.Psw = "Admin1601";
            usrAdmin.Estado = "S";
            usrAdmin.FechaAlta = DateTime.Now;
            usrAdmin.FechaBaja = new DateTime(2000, 01, 01);
            usrAdmin.Direccion = "N/A";
            usrAdmin.CUIL = "N/A";
            usrAdmin.Telefono = "N/A";
            usrAdmin.RazonSocial = "N/A";
            usrAdmin.Idioma = new Idioma { Id = 1, Descripcion = "Español", Abreviacion = "Esp" };
            usrAdmin.PerfilUsr = new PerfilUsr { Id = 1, Descripcion = "WebMaster" };
            usrAdmin.Localidad = new Localidad { Id = 1, Descripcion = "Implantagraf" };

            var aud = new Auditoria();
            var inte = new IntegridadDatos();
                       
            
            var usuarioActual = accDatosUSr.RegistrarUsuario(usrAdmin);

            var usuarioActualDVH = inte.CalcularDVH(usuarioActual.Id.ToString() + priv.Cifrar(usuarioActual.RazonSocial) + priv.Cifrar(usuarioActual.Nombre) + priv.Cifrar(usuarioActual.Apellido) + priv.Cifrar(usuarioActual.Usr) + priv.Cifrar(usuarioActual.Psw) + priv.Cifrar(usuarioActual.CUIL) + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + priv.Cifrar(usuarioActual.Telefono) + priv.Cifrar(usuarioActual.Direccion));

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHUsuario(usuarioActual.Id, usuarioActualDVH);
            inte.RecalcularDVV("SEG_Usuario");

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, "SISTEMA", "ALTA USUARIO", "INFO", "Se registró al Usuario: " + usuarioActual.Id.ToString() + " - '" + usuarioActual.Usr + "' con el perfil de " + usuarioActual.PerfilUsr.Descripcion);

            return usuarioActual;

        }

        public bool ValidarExistenciaTablas(bool flag)
        {
            string[] tablasImplantagraf = { "Categoria", "Cliente", "DetalleOperacion", "EstadoOperacion",
                                            "Factura", "FormaPago", "Idioma", "Localidad", "Marca", "Operacion",
                                            "Producto", "SEG_Bitacora", "SEG_DetallePermisos", "SEG_DVV","SEG_IntegridadRegistros",
                                            "SEG_PerfilUsr", "SEG_Permisos", "SEG_Usuario", 
                                            "WS_Empresa_TC", "WS_Marca_TC"};


            for (int i = 0; i < tablasImplantagraf.Length; i++)
            {
                string tabla = tablasImplantagraf[i];

                if (ValidarExistencia(tabla) == 0)

                {
                    GrabarRegistroIntegridad("SE ELIMINÓ", tabla.ToUpper());
                    flag = true;
                }

            }

            //Retorna true cuando falla la integridad.
            return flag;
        }

        public bool ValidarTablasDVV(bool flag)
        {
            string[] tablasDVV = { "Categoria", "Cliente", "DetalleOperacion",
                                   "Factura", "FormaPago", "Idioma", "Localidad", "Marca", "Operacion",
                                   "Producto", "SEG_Bitacora", "SEG_PerfilUsr",
                                   "SEG_Permisos", "SEG_DetallePermisos","SEG_Usuario"};
            long DVV = 0;

            for (int i = 0; i < tablasDVV.Length; i++)
            {
                string tabla = tablasDVV[i];
                var cant = 0;

                if (ValidarExistencia(tabla) != 0)
                {
                    if (ExisteRegTablaDVV(tabla) != 0)
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
                                    GrabarRegistroIntegridad("SE ALTERÓ EL Nº DE REGISTROS", tabla.ToUpper());
                                    flag = true;
                                }
                            }

                        }

                    }
                    else
                    {
                        GrabarRegistroIntegridad("SE ELIMINÓ REGISTRO", "SEG_DVV", tabla.ToUpper());
                        flag = true;
                    }

                }

            }

            return flag;
        }

        private void CreoTablaUsuario()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaUsuario();

        }

        public int ExisteRegTablaDVV(string tabla)
        {
            var integ = new IntegridadDAC();

            return integ.ExisteRegTablaDVV(tabla);

        }

        private void CreoTablaDVV()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaDVV();

        }

        private void CreoTablaTraductor()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaTraductor();

        }

        private void CreoTablaLocalidad()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaLocalidad();

        }

        private void CreoTablaPerfilUsr()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaPerfilUsr();

        }

        private void CreoTablaPermisosUsr()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaPermisosUsr();

        }
        private void CreoTablaDetallePermisosUsr()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaDetallePermisosUsr();

        }
        private void CreoTablaIdioma()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaIdioma();

        }

        public void RecalcularDVV()
        {
            string[] tablasDVV = { "Categoria", "Cliente", "DetalleOperacion",
                                   "Factura", "FormaPago", "Idioma", "Localidad", "Marca", "Operacion",
                                   "Producto", "SEG_Bitacora", "SEG_PerfilUsr",
                                   "SEG_Permisos", "SEG_DetallePermisos", "SEG_Usuario", "Stock"};
            long DVV = 0;

            for (int i = 0; i < tablasDVV.Length; i++)
            {
                string tabla = tablasDVV[i];

                if (ExisteRegTablaDVV(tabla) != 0)
                {
                    if (ValidarExistencia(tabla) != 0)
                    {
                        DVV = CalcularDVV(tabla);

                        var CantReg = ContarRegistros(tabla);

                        ActualizarDVV(tabla, DVV, CantReg);
                    }
                }
            }

        }

        public void RecalcularDVV(string tabla)
        {
            if (ExisteRegTablaDVV(tabla) != 0 && ValidarExistencia(tabla) != 0)
            {
                var DVV = CalcularDVV(tabla);

                var cantReg = ContarRegistros(tabla);

                ActualizarDVV(tabla, DVV, cantReg);
            }
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

            if (ValidarExistencia("SEG_DVV") != 0)
            { accDatos.ActualizarDVV(tabla, DVV, CantReg); }

        }

        public int ContarRegistros(string tabla)
        {
            var accDatos = new IntegridadDAC();

            return accDatos.ContarRegistros(tabla);
        }

        public int ValidarExistencia(string tabla)
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

        public void GrabarRegistroIntegridad(string Col_A = "N/A", string Col_B = "N/A", string Col_C = "N/A", string Col_D = "N/A", string Col_E = "N/A", string Col_F = "N/A", string Col_G = "N/A")
        {
            var accDatos = new IntegridadDAC();

            if (ValidarExistencia("SEG_IntegridadRegistros") == 0)
            { CreoTablaIntegridad(); }

            if (accDatos.ExisteRegistroIntegridad(Col_A, Col_B, Col_C) == 0)
            {
                accDatos.grabarRegistroIntegridad(Col_A, Col_B, Col_C, Col_D, Col_E, Col_F, Col_G);
            }
        }


        private void CreoTablaIntegridad()
        {
            var integ = new IntegridadDAC();

            integ.CreoTablaIntegridad();

        }

        public List<IntegridadRegistros> ListarRegistrosTablasFaltantes()
        {
            var accDatos = new IntegridadDAC();

            return accDatos.ListarRegistrosTablasFaltantes();

        }

        public void LimpiarTablaRegistrosTablasFaltantes()
        {
            var accDatos = new IntegridadDAC();

            if (ValidarExistencia("SEG_IntegridadRegistros") != 0)
            { accDatos.LimpiarTablaRegistrosTablasFaltantes(); }

        }

        public bool ValidaSoloIdioma(bool flag)
        {
            var accDatosIdioma = new IdiomaDAC();

            var listadoIdiomas = accDatosIdioma.Listar();

            foreach (Idioma idiomaActual in listadoIdiomas)
            {
                if (CalcularDVH(idiomaActual.Id.ToString() + idiomaActual.Descripcion + idiomaActual.Abreviacion) != idiomaActual.DVH)
                {
                    GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "IDIOMA", idiomaActual.Id.ToString(), idiomaActual.Descripcion, idiomaActual.Abreviacion);
                    flag = true;
                }
            }

            return flag;
        }

        public bool ValidaSoloLocalidad(bool flag)
        {
            var accDatosLocalidad = new LocalidadDAC();

            var listadoLocalidades = accDatosLocalidad.Listar();

            foreach (Localidad localidadActual in listadoLocalidades)
            {
                if (CalcularDVH(localidadActual.Id.ToString() + localidadActual.Descripcion) != localidadActual.DVH)
                {
                    GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "LOCALIDAD", localidadActual.Id.ToString(), localidadActual.Descripcion);
                    flag = true;
                }
            }

            return flag;
        }

        public bool ValidaSoloPerfilUsr(bool flag)
        {
            var accDatosPerfiles = new PerfilUsrDAC();

            var listadoPerfiles = accDatosPerfiles.Listar();

            foreach (PerfilUsr perfilActual in listadoPerfiles)
            {
                if (CalcularDVH(perfilActual.Id.ToString() + perfilActual.Descripcion) != perfilActual.DVH)
                {
                    GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "SEG_PERFILUSR", perfilActual.Id.ToString(), perfilActual.Descripcion);
                    flag = true;
                }
            }
            return flag;
        }

        public bool ValidaSoloPermisosUsr(bool flag)
        {
            var accDatos = new CuentaDAC();

            var listadoPermisos = accDatos.ListarPermisos();

            foreach (PermisosUsr permisoActual in listadoPermisos)
            {
                if (CalcularDVH(permisoActual.Id.ToString() + permisoActual.Descripcion) != permisoActual.DVH)
                {
                    GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "SEG_PERMISOS", permisoActual.Id.ToString(), permisoActual.Descripcion);
                    flag = true;
                }
            }

            return flag;
        }



        public bool ValidarRegistrosDVH(bool flag)
        {
            var priv = new Privacidad();

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
            var accDatosPerfiles = new PerfilUsrDAC();
            var accDatosStock = new StockDAC();
            var accDatosEstadoOperacion = new EstadoOperacionDAC();

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
            List<PermisosUsr> listadoPermisos = new List<PermisosUsr>();
            List<DetallePermisoUsr> listadoDetallePermisos = new List<DetallePermisoUsr>();
            List<PerfilUsr> listadoPerfiles = new List<PerfilUsr>();
            List<Stock> listadoStock = new List<Stock>();
            List<EstadoOperacion> listadoEstadoOperacion = new List<EstadoOperacion>();

            if (ValidarExistencia("SEG_Usuario") == 1)
            {
                // Usuarios.
                listadoUsuarios = accDatosUsuario.ListarUsuarios();

                foreach (Usuario usuarioActual in listadoUsuarios)
                {
                    if (CalcularDVH(usuarioActual.Id.ToString() + priv.Cifrar(usuarioActual.RazonSocial) + priv.Cifrar(usuarioActual.Nombre) + priv.Cifrar(usuarioActual.Apellido) + priv.Cifrar(usuarioActual.Usr) + priv.Cifrar(usuarioActual.Psw) + priv.Cifrar(usuarioActual.CUIL) + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + priv.Cifrar(usuarioActual.Telefono) + priv.Cifrar(usuarioActual.Direccion)) != usuarioActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "SEG_USUARIO", usuarioActual.Id.ToString(), usuarioActual.RazonSocial, usuarioActual.CUIL, usuarioActual.PerfilUsr.Descripcion, usuarioActual.Usr);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Producto") == 1)
            {
                // Productos
                listadoProductos = accDatosProductos.ListarProductos();

                foreach (Producto productoActual in listadoProductos)
                {
                    if (CalcularDVH(productoActual.Codigo + productoActual.Titulo + productoActual.Titulo_Eng + productoActual.Modelo + productoActual.Descripcion + productoActual.Descripcion_Eng + productoActual.Imagen + productoActual.Marca.Id.ToString() + productoActual.Categoria.Id.ToString() + productoActual.Precio.ToString()) != productoActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "PRODUCTO", productoActual.Codigo.ToString(), productoActual.Titulo, productoActual.Modelo, productoActual.Marca.Descripcion, productoActual.Categoria.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Categoria") == 1)
            {
                // Categorias.
                listadoCategorias = accDatosCategorias.Listar();

                foreach (Categoria categoriaActual in listadoCategorias)
                {
                    if (CalcularDVH(categoriaActual.Id.ToString() + categoriaActual.Descripcion) != categoriaActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "CATEGORIA", categoriaActual.Id.ToString(), categoriaActual.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Cliente") == 1)
            {
                // Clientes
                listadoClientes = accDatosClientes.Listar();

                foreach (Cliente clienteActual in listadoClientes)
                {
                    if (CalcularDVH(clienteActual.Id.ToString() + priv.Cifrar(clienteActual.RazonSocial) + priv.Cifrar(clienteActual.CUIL) + priv.Cifrar(clienteActual.Email) + priv.Cifrar(clienteActual.Telefono) + priv.Cifrar(clienteActual.Direccion) + clienteActual.FechaAlta.ToString() + clienteActual.Localidad.Id.ToString()) != clienteActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "CLIENTE", clienteActual.Id.ToString(), clienteActual.RazonSocial, clienteActual.CUIL, clienteActual.Email, clienteActual.FechaAlta.ToString());
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("DetalleOperacion") == 1)
            {
                // Detalle Operacion
                listadoDetalleOperaciones = accDatosOperaciones.ListarDetalleOperacion();

                foreach (DetalleOperacion detalleActual in listadoDetalleOperaciones)
                {
                    if (CalcularDVH(detalleActual.OperacionId.ToString() + detalleActual.ProductoId.ToString() + detalleActual.SubTotal.ToString() + detalleActual.Cantidad.ToString() + detalleActual.Monto.ToString()) != detalleActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "DETALLEOPERACION", detalleActual.OperacionId.ToString(), detalleActual.ProductoId.ToString(), detalleActual.SubTotal.ToString());
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Factura") == 1)
            {
                // Facturas
                listadoFacturas = accDatosOperaciones.ListarFacturas();

                foreach (Factura facturaActual in listadoFacturas)
                {
                    if (CalcularDVH(facturaActual.Codigo.ToString() + facturaActual.FechaHora.ToString() + facturaActual.Tipo + facturaActual.Cliente.Id.ToString() + facturaActual.Monto.ToString() + facturaActual.FormaPago.Id.ToString() + facturaActual.Estado.Id.ToString()) != facturaActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "FACTURA", facturaActual.Codigo.ToString(), facturaActual.Cliente.Id.ToString(), facturaActual.Monto.ToString());
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("FormaPago") == 1)
            {
                // Formas de Pago
                listadoFormasPago = accDatosFormaPago.Listar();

                foreach (FormaPago formaPagoActual in listadoFormasPago)
                {
                    if (CalcularDVH(formaPagoActual.Id.ToString() + formaPagoActual.Descripcion) != formaPagoActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "FORMAPAGO", formaPagoActual.Id.ToString(), formaPagoActual.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("EstadoOperacion") == 1)
            {
                // Estado Operacion
                listadoEstadoOperacion = accDatosEstadoOperacion.Listar();

                foreach (EstadoOperacion estadoOperacionActual in listadoEstadoOperacion)
                {
                    if (CalcularDVH(estadoOperacionActual.Id.ToString() + estadoOperacionActual.Descripcion) != estadoOperacionActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "ESTADOOPERACION", estadoOperacionActual.Id.ToString(), estadoOperacionActual.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Idioma") == 1)
            {
                // Idioma
                listadoIdiomas = accDatosIdioma.Listar();

                foreach (Idioma idiomaActual in listadoIdiomas)
                {
                    if (CalcularDVH(idiomaActual.Id.ToString() + idiomaActual.Descripcion + idiomaActual.Abreviacion) != idiomaActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "IDIOMA", idiomaActual.Id.ToString(), idiomaActual.Descripcion, idiomaActual.Abreviacion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Localidad") == 1)
            {
                // Localidad
                listadoLocalidades = accDatosLocalidad.Listar();

                foreach (Localidad localidadActual in listadoLocalidades)
                {
                    if (CalcularDVH(localidadActual.Id.ToString() + localidadActual.Descripcion) != localidadActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "LOCALIDAD", localidadActual.Id.ToString(), localidadActual.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Marca") == 1)
            {
                // Marca
                listadoMarcas = accDatosMarca.Listar();

                foreach (Marca marcaActual in listadoMarcas)
                {
                    if (CalcularDVH(marcaActual.Id.ToString() + marcaActual.Descripcion) != marcaActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "MARCA", marcaActual.Id.ToString(), marcaActual.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Operacion") == 1)
            {
                // Operaciones
                listadoOperaciones = accDatosOperaciones.ListarOperaciones();

                foreach (Operacion operacionActual in listadoOperaciones)
                {
                    if (CalcularDVH(operacionActual.Id.ToString() + operacionActual.Cliente.Id.ToString() + operacionActual.FechaHora.ToString() + operacionActual.TipoOperacion + operacionActual.ImporteTotal.ToString() + operacionActual.Factura.Codigo.ToString() + operacionActual.Estado.Id.ToString()) != operacionActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "OPERACIÓN", operacionActual.Id.ToString(), operacionActual.Cliente.Id.ToString(), operacionActual.FechaHora.ToString(), operacionActual.ImporteTotal.ToString(), operacionActual.Factura.Codigo.ToString());
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Bitacora") == 1)
            {
                // Bitácora
                listadoBitacora = accDatosBitacora.ConsultarBitacora();

                foreach (Bitacora bitacoraActual in listadoBitacora)
                {

                    if (CalcularDVH(bitacoraActual.FechaHora.ToString() + bitacoraActual.Usuario + bitacoraActual.Accion + bitacoraActual.Criticidad + bitacoraActual.Detalle) != bitacoraActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "SEG_BITACORA", bitacoraActual.FechaHora.ToString(), bitacoraActual.Accion, bitacoraActual.FechaHora.ToString(), bitacoraActual.Criticidad, bitacoraActual.Detalle);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("SEG_Permisos") == 1)
            {
                // Permisos
                listadoPermisos = accDatosUsuario.ListarPermisos();

                foreach (PermisosUsr permisoActual in listadoPermisos)
                {
                    if (CalcularDVH(permisoActual.Id.ToString() + permisoActual.Descripcion) != permisoActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "SEG_PERMISOS", permisoActual.Id.ToString(), permisoActual.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                //Detalle Permisos
                listadoDetallePermisos = accDatosUsuario.ListarDetallePermisos();

                foreach (DetallePermisoUsr detallePermisoActual in listadoDetallePermisos)
                {
                    if (CalcularDVH(detallePermisoActual.Id.ToString() + detallePermisoActual.UsrId.ToString() + detallePermisoActual.PermisoId.ToString() + detallePermisoActual.Otorgado) != detallePermisoActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "SEG_DETALLEPERMISOS", detallePermisoActual.Id.ToString());
                        flag = true;
                    }
                }
            }


            if (ValidarExistencia("SEG_PerfilUsr") == 1)
            {

                // Perfiles de Usuario
                listadoPerfiles = accDatosPerfiles.Listar();

                foreach (PerfilUsr perfilActual in listadoPerfiles)
                {
                    if (CalcularDVH(perfilActual.Id.ToString() + perfilActual.Descripcion) != perfilActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "SEG_PERFILUSR", perfilActual.Id.ToString(), perfilActual.Descripcion);
                        flag = true;
                    }
                }
            }

            if (ValidarExistencia("Stock") == 1)
            {
                // Stock
                listadoStock = accDatosStock.VerStock();

                foreach (Stock stockActual in listadoStock)
                {
                    if (CalcularDVH(stockActual.ProductoId.ToString() + stockActual.FechaCalendario.ToString() + stockActual.Cantidad.ToString() + stockActual.TipoOperacionId.ToString()) != stockActual.DVH)
                    {
                        GrabarRegistroIntegridad("SE ALTERÓ REGISTRO", "STOCK", stockActual.ProductoId.ToString(), stockActual.FechaCalendario.ToString(), stockActual.Cantidad.ToString(), stockActual.TipoOperacionId.ToString());
                        flag = true;
                    }
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

        public void ActualizarDVHCategoria(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHCategoria(id, DVH);
        }

        public void ActualizarDVHCliente(int cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHCliente(cod, DVH);
        }

        public void ActualizarDVHDetalleOperacion(int opeId, int prodId, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHDetalleOperacion(opeId, prodId, DVH);
        }

        public void ActualizarDVHFactura(int cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHFactura(cod, DVH);
        }

        public void ActualizarDVHFormaPago(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHFormaPago(id, DVH);
        }

        public void ActualizarDVHEstadoOperacion(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHEstadoOperacion(id, DVH);
        }

        public void ActualizarDVHIdioma(int cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHIdioma(cod, DVH);
        }

        public void ActualizarDVHLocalidad(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHLocalidad(id, DVH);
        }

        public void ActualizarDVHMarca(int cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHMarca(cod, DVH);
        }

        public void ActualizarDVHOperacion(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHOperacion(id, DVH);
        }

        public void ActualizarDVHBitacora(long cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHBitacora(cod, DVH);
        }

        public void ActualizarDVHPerfilUsr(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHPerfilUsr(id, DVH);
        }

        public void ActualizarDVHPermisos(int cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHPermisos(cod, DVH);
        }

        public void ActualizarDVHDetallePermisos(int cod, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHDetallePermisos(cod, DVH);
        }

        public void ActualizarDVHStock(int id, long DVH)
        {
            var integ = new IntegridadDAC();

            integ.ActualizarDVHStock(id, DVH);
        }

        public void RecalcularTodosDVH()
        {
            long dvhActual;

            var priv = new Privacidad();

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
            var accDatosPerfiles = new PerfilUsrDAC();
            var accDatosStock = new StockDAC();
            var accDatosEstadoOperacion = new EstadoOperacionDAC();

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
            List<PermisosUsr> listadoPermisos = new List<PermisosUsr>();
            List<DetallePermisoUsr> listadoDetallePermisos = new List<DetallePermisoUsr>();
            List<PerfilUsr> listadoPerfiles = new List<PerfilUsr>();
            List<Stock> listadoStock = new List<Stock>();
            List<EstadoOperacion> listadoEstadoOperacion = new List<EstadoOperacion>();

            if (ValidarExistencia("SEG_Usuario") == 1)
            {
                // Usuarios.
                listadoUsuarios = accDatosUsuario.ListarUsuarios();

                foreach (Usuario usuarioActual in listadoUsuarios)
                {
                    dvhActual = CalcularDVH(usuarioActual.Id.ToString() + priv.Cifrar(usuarioActual.RazonSocial) + priv.Cifrar(usuarioActual.Nombre) + priv.Cifrar(usuarioActual.Apellido) + priv.Cifrar(usuarioActual.Usr) + priv.Cifrar(usuarioActual.Psw) + priv.Cifrar(usuarioActual.CUIL) + usuarioActual.PerfilUsr.Id.ToString() + usuarioActual.Idioma.Id.ToString() + usuarioActual.Localidad.Id.ToString() + usuarioActual.FechaAlta.ToString() + usuarioActual.FechaBaja.ToString() + priv.Cifrar(usuarioActual.Telefono) + priv.Cifrar(usuarioActual.Direccion));

                    ActualizarDVHUsuario(usuarioActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("Producto") == 1)
            {
                // Productos.
                listadoProductos = accDatosProductos.ListarProductos();

                foreach (Producto productoActual in listadoProductos)
                {
                    dvhActual = CalcularDVH(productoActual.Codigo + productoActual.Titulo + productoActual.Titulo_Eng + productoActual.Modelo + productoActual.Descripcion + productoActual.Descripcion_Eng + productoActual.Imagen + productoActual.Marca.Id.ToString() + productoActual.Categoria.Id.ToString() + productoActual.Precio.ToString());

                    ActualizarDVHProducto(productoActual.Codigo, dvhActual);

                }
            }

            if (ValidarExistencia("Categoria") == 1)
            {
                // Categorias.
                listadoCategorias = accDatosCategorias.Listar();

                foreach (Categoria categoriaActual in listadoCategorias)
                {
                    dvhActual = CalcularDVH(categoriaActual.Id.ToString() + categoriaActual.Descripcion);

                    ActualizarDVHCategoria(categoriaActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("Cliente") == 1)
            {
                // Clientes
                listadoClientes = accDatosClientes.Listar();

                foreach (Cliente clienteActual in listadoClientes)
                {
                    dvhActual = CalcularDVH(clienteActual.Id.ToString() + priv.Cifrar(clienteActual.RazonSocial) + priv.Cifrar(clienteActual.CUIL) + priv.Cifrar(clienteActual.Email) + priv.Cifrar(clienteActual.Telefono) + priv.Cifrar(clienteActual.Direccion) + clienteActual.FechaAlta.ToString() + clienteActual.Localidad.Id.ToString());
                    
                    ActualizarDVHCliente(clienteActual.Id, dvhActual);

                }
            }

            if (ValidarExistencia("DetalleOperacion") == 1)
            {
                // Detalle Operacion
                listadoDetalleOperaciones = accDatosOperaciones.ListarDetalleOperacion();

                foreach (DetalleOperacion detalleActual in listadoDetalleOperaciones)
                {
                    dvhActual = CalcularDVH(detalleActual.OperacionId.ToString() + detalleActual.ProductoId.ToString() + detalleActual.SubTotal.ToString() + detalleActual.Cantidad.ToString() + detalleActual.Monto.ToString());

                    ActualizarDVHDetalleOperacion(detalleActual.OperacionId, detalleActual.ProductoId, dvhActual);
                }
            }

            if (ValidarExistencia("Factura") == 1)
            {
                // Facturas
                listadoFacturas = accDatosOperaciones.ListarFacturas();

                foreach (Factura facturaActual in listadoFacturas)
                {
                    dvhActual = CalcularDVH(facturaActual.Codigo.ToString() + facturaActual.FechaHora.ToString() + facturaActual.Tipo + facturaActual.Cliente.Id.ToString() + facturaActual.Monto.ToString() + facturaActual.FormaPago.Id.ToString() + facturaActual.Estado.Id.ToString());

                    ActualizarDVHFactura(facturaActual.Codigo, dvhActual);
                }
            }

            if (ValidarExistencia("FormaPago") == 1)
            {
                // Formas de Pago
                listadoFormasPago = accDatosFormaPago.Listar();

                foreach (FormaPago formaPagoActual in listadoFormasPago)
                {
                    dvhActual = CalcularDVH(formaPagoActual.Id.ToString() + formaPagoActual.Descripcion);

                    ActualizarDVHFormaPago(formaPagoActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("EstadoOperacion") == 1)
            {
                // Estado Operacion
                listadoEstadoOperacion = accDatosEstadoOperacion.Listar();

                foreach (EstadoOperacion estadoOperacionActual in listadoEstadoOperacion)
                {
                    dvhActual = CalcularDVH(estadoOperacionActual.Id.ToString() + estadoOperacionActual.Descripcion);

                    ActualizarDVHEstadoOperacion(estadoOperacionActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("Idioma") == 1)
            {
                // Idioma
                listadoIdiomas = accDatosIdioma.Listar();

                foreach (Idioma idiomaActual in listadoIdiomas)
                {
                    dvhActual = CalcularDVH(idiomaActual.Id.ToString() + idiomaActual.Descripcion + idiomaActual.Abreviacion);

                    ActualizarDVHIdioma(idiomaActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("Localidad") == 1)
            {
                // Localidad
                listadoLocalidades = accDatosLocalidad.Listar();

                foreach (Localidad localidadActual in listadoLocalidades)
                {
                    dvhActual = CalcularDVH(localidadActual.Id.ToString() + localidadActual.Descripcion);

                    ActualizarDVHLocalidad(localidadActual.Id, dvhActual);
                }
            }


            if (ValidarExistencia("Marca") == 1)
            {
                // Marca
                listadoMarcas = accDatosMarca.Listar();

                foreach (Marca marcaActual in listadoMarcas)
                {
                    dvhActual = CalcularDVH(marcaActual.Id.ToString() + marcaActual.Descripcion);

                    ActualizarDVHMarca(marcaActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("Operacion") == 1)
            {
                // Operaciones
                listadoOperaciones = accDatosOperaciones.ListarOperaciones();

                foreach (Operacion operacionActual in listadoOperaciones)
                {
                    dvhActual = CalcularDVH(operacionActual.Id.ToString() + operacionActual.Cliente.Id.ToString() + operacionActual.FechaHora.ToString() + operacionActual.TipoOperacion + operacionActual.ImporteTotal.ToString() + operacionActual.Factura.Codigo.ToString() + operacionActual.Estado.Id.ToString());

                    ActualizarDVHOperacion(operacionActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("Bitacora") == 1)
            {
                // Bitácora
                listadoBitacora = accDatosBitacora.ConsultarBitacora();

                foreach (Bitacora bitacoraActual in listadoBitacora)
                {
                    dvhActual = CalcularDVH(bitacoraActual.FechaHora.ToString() + bitacoraActual.Usuario + bitacoraActual.Accion + bitacoraActual.Criticidad + bitacoraActual.Detalle);

                    ActualizarDVHBitacora(bitacoraActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("SEG_Permisos") == 1)
            {
                // Permisos
                listadoPermisos = accDatosUsuario.ListarPermisos();

                foreach (PermisosUsr permisoActual in listadoPermisos)
                {
                    dvhActual = CalcularDVH(permisoActual.Id.ToString() + permisoActual.Descripcion);

                    ActualizarDVHPermisos(permisoActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("SEG_DetallePermisos") == 1)
            {
                // DetallePermisos
                listadoDetallePermisos = accDatosUsuario.ListarDetallePermisos();

                foreach (DetallePermisoUsr detallePermisoActual in listadoDetallePermisos)
                {
                    dvhActual = CalcularDVH(detallePermisoActual.Id.ToString() + detallePermisoActual.UsrId.ToString() + detallePermisoActual.PermisoId.ToString() + detallePermisoActual.Otorgado);

                    ActualizarDVHDetallePermisos(detallePermisoActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("SEG_PerfilUsr") == 1)
            {
                // Perfiles de Usuario
                listadoPerfiles = accDatosPerfiles.Listar();

                foreach (PerfilUsr perfilActual in listadoPerfiles)
                {
                    dvhActual = CalcularDVH(perfilActual.Id.ToString() + perfilActual.Descripcion);

                    ActualizarDVHPerfilUsr(perfilActual.Id, dvhActual);
                }
            }

            if (ValidarExistencia("Stock") == 1)
            {

                // Stock
                listadoStock = accDatosStock.VerStock();

                foreach (Stock stockActual in listadoStock)
                {
                    dvhActual = CalcularDVH(stockActual.ProductoId.ToString() + stockActual.FechaCalendario + stockActual.Cantidad.ToString() + stockActual.TipoOperacionId.ToString());

                    ActualizarDVHStock(stockActual.Id, dvhActual);
                }
            }

            RecalcularDVV("Categoria");
            RecalcularDVV("Cliente");
            RecalcularDVV("DetalleOperacion");
            RecalcularDVV("Factura");
            RecalcularDVV("FormaPago");
            RecalcularDVV("Idioma");
            RecalcularDVV("Localidad");
            RecalcularDVV("Marca");
            RecalcularDVV("Operacion");
            RecalcularDVV("Producto");
            RecalcularDVV("SEG_Bitacora");
            RecalcularDVV("SEG_PerfilUsr");
            RecalcularDVV("SEG_Permisos");
            RecalcularDVV("SEG_Usuario");
            RecalcularDVV("SEG_DetallePermisos");
            RecalcularDVV("Stock");
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


        private void InsertarIdiomaCompleto()
        {

            var integridad = new IntegridadDAC();

            integridad.InsertarIdiomaCompleto();


        }

        private void InsertarPerfilUsrCompleto()
        {

            var integridad = new IntegridadDAC();

            integridad.InsertarPerfilUsrCompleto();
        }

        private void InsertarPermisosCompleto()
        {

            var integridad = new IntegridadDAC();

            integridad.InsertarPermisosCompleto();
        }

        private void InsertarLocalidadCompleto()
        {

            var integridad = new IntegridadDAC();

            integridad.InsertarLocalidadCompleto();

        }


    }
}