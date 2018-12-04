using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace AccesoDatos
{
    public class IntegridadDAC : DataAccessComponent
    {

        public int ValidarExistenciaBase()
        {

            const string sqlStatement = "USE [master]" +
                                        "SELECT COUNT(*) FROM sysdatabases WHERE(name = 'Implantagraf');";

            var db = DatabaseFactory.CreateDatabase(ConnectionNameMaster);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
        }

        public void CrearBaseImplantagraf()
        {
            const string sqlStatement = "USE [master]" +
                                        "CREATE DATABASE Implantagraf;";

            var db = DatabaseFactory.CreateDatabase(ConnectionNameMaster);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public void CreoTablaTraductor()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE [dbo].[Traductor](" +
                                        "[Elemento][nvarchar](100) NOT NULL," +
                                        "[Esp] [nvarchar](1000) NULL," +
                                        "[Eng][nvarchar](1000) NULL," +
                                        "[Bra] [nvarchar](1000) NULL," +
                                        "CONSTRAINT[PK_Traductor] PRIMARY KEY CLUSTERED(   [Elemento] ASC" +
                                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                                        ") ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void CreoTablaDVV()
        {
            const string sqlStatement = "CREATE TABLE [dbo].[SEG_DVV](" +
                                        "[Tabla] [varchar](100) NOT NULL," +
                                        "[DVV] [bigint] NOT NULL," +
                                        "[CantidadReg] [int] NOT NULL," +
                                        "CONSTRAINT[PK_SEG_DVV] PRIMARY KEY CLUSTERED([Tabla] ASC)" +
                                        "WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                                        ") ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void CreoTablaIdioma()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE [dbo].[Idioma](" +
                                        "[Id][int] NOT NULL," +
                                        "[Descripcion] [nvarchar](50) NOT NULL," +
                                        "[Abreviacion] [nvarchar](3) NOT NULL," +
                                        "[DVH] [bigint] NOT NULL) ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void CreoTablaLocalidad()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE [dbo].[Localidad](" +
                                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                                        "[Descripcion] [nvarchar](60) NOT NULL," +
                                        "[DVH] [bigint] NOT NULL," +
                                        "CONSTRAINT[PK_Localidad] PRIMARY KEY CLUSTERED([Id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, " +
                                        "IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void CreoTablaPerfilUsr()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE [dbo].[SEG_PerfilUsr](" +
                                        "[Id][int] NOT NULL," +
                                        "[Descripcion] [varchar](50) NOT NULL," +
                                        "[DVH] [bigint] NOT NULL," +
                                        "CONSTRAINT[PK_SEG_Perfil] PRIMARY KEY CLUSTERED([Id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, " +
                                        "IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void CreoTablaUsuario()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE[dbo].[SEG_Usuario]([Id][int] IDENTITY(1, 1) NOT NULL, " +
                                        "[RazonSocial] [varchar](50) NOT NULL, " +
                                        "[Nombre] [varchar](50) NOT NULL, " +
                                        "[Apellido] [varchar](50) NOT NULL, " +
                                        "[Usr] [varchar](50) NOT NULL, " +
                                        "[Psw] [varchar](50) NOT NULL, " +
                                        "[CUIL] [varchar](11) NOT NULL, " +
                                        "[Estado] [char](1) NOT NULL, " +
                                        "[Intentos] [bigint] NOT NULL, " +
                                        "[Email] [varchar](100) NOT NULL, " +
                                        "[Telefono] [varchar](25) NOT NULL, " +
                                        "[Direccion] [varchar](100) NOT NULL, " +
                                        "[LocalidadId] [int] NOT NULL, " +
                                        "[PerfilId] [int] NOT NULL, " +
                                        "[IdiomaId] [int] NOT NULL, " +
                                        "[FechaAlta] [datetime] NOT NULL, " +
                                        "[FechaBaja] [datetime] NOT NULL, " +
                                        "[DVH] [bigint] NOT NULL, " +
                                        "CONSTRAINT[PK_SEG_Usuario] PRIMARY KEY CLUSTERED(" +
                                        "[Id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,  " +
                                        "IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] ) ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void CreoTablaPermisosUsr()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE[dbo].[SEG_Permisos](" +
                                        "[Id][int] NOT NULL," +
                                        "[Descripcion] [nvarchar](50) NOT NULL," +
                                        "[DVH] [bigint] NOT NULL," +
                                        "CONSTRAINT[PK_SEG_Permisos] PRIMARY KEY CLUSTERED" +
                                        "([Id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, " +
                                        "IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY];";


            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void CreoTablaDetallePermisosUsr()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE [dbo].[SEG_DetallePermisos](" +
                                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                                        "[UsrId] [int] NOT NULL," +
                                        "[PermisoId] [int] NOT NULL," +
                                        "[Otorgado] [nvarchar](1) NOT NULL," +
                                        "[DVH] [bigint] NOT NULL," +
                                        "CONSTRAINT[PK_SEG_DetallePermisos] PRIMARY KEY CLUSTERED" +
                                        "([Id] ASC) WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, " +
                                        "ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }


        public void CreoTablaIntegridad()
        {
            const string sqlStatement = "USE [Implantagraf]" +
                                        "CREATE TABLE [dbo].[SEG_IntegridadRegistros](" +
                                        "[Col_A][varchar](100) NULL," +
                                        "[Col_B][varchar](100) NULL," +
                                        "[Col_C][varchar](100) NULL," +
                                        "[Col_D][varchar](100) NULL," +
                                        "[Col_E][varchar](100) NULL," +
                                        "[Col_F][varchar](100) NULL," +
                                        "[Col_G][varchar](100) NULL" +
                                        ") ON[PRIMARY];";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }


        }

        public void InsertarIdiomaCompleto()
        {
            const string sqlStatement = "DELETE FROM Idioma; " +
                                        "INSERT [dbo].[Idioma] ([Id], [Descripcion], [Abreviacion], [DVH]) VALUES (1, N'Español', N'Esp', 1020);" +
                                        "INSERT[dbo].[Idioma]([Id], [Descripcion], [Abreviacion], [DVH]) VALUES(2, N'English', N'Eng', 1046);";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public void InsertarPermisosCompleto()
        {
            const string sqlStatement = "DELETE FROM SEG_Permisos; " +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (1,'Ver Carrito de Compras',2096);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (2,'Agregar Producto al Carrito',2620);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (3,'Realizar Compras',1634);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (4,'Realizar Pagos',1416);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (5,'Ver Página de Contacto',2036);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (6,'Ver Página de Información Empresarial',3476);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (7,'Consultar la Bitácora',2050);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (8,'Realizar Restore de la Base',2535);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (9,'Realizar Copia de Respaldo de la Base',3379);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (10,'Bloquear Cuenta de Usuario',2573);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (11,'Desbloquear Cuenta de Usuario',2890);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (12,'ABM de Objetos de Negocio',2271);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (13,'Generar Reporte de Ventas',2467);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (14,'Generar Reporte de Stock',2359);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (15,'Generar Reporte de Clientes',2667);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (16,'Solucionar Problemas de Integridad',3407);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (17,'Ver Informacion de Cuenta de Usaurio',3460);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (18,'Suscribirse al Voletin de Promociones',3708);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (19,'ABM de Usuarios',1438);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (20,'Consultar Compras Realizadas',2866);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (21,'Cancelar Facturas',1749);" +
                                        "INSERT [dbo].[SEG_Permisos] ([Id], [Descripcion], [DVH]) VALUES (22,'Cambiar Idioma',1414);";



            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public void InsertarLocalidadCompleto()
        {
            const string sqlStatement = "DELETE FROM Localidad; " +
                                        "SET IDENTITY_INSERT [dbo].[Localidad] ON " +
                                        "INSERT [dbo].[Localidad] ([Id], [Descripcion], [DVH]) VALUES (1, N'Implantagraf', 1287);" +
                                        "INSERT [dbo].[Localidad] ([Id], [Descripcion], [DVH]) VALUES (2, N'Castelar', 865);" +
                                        "INSERT [dbo].[Localidad] ([Id], [Descripcion], [DVH]) VALUES (3, N'Haedo', 532);" +
                                        "INSERT [dbo].[Localidad] ([Id], [Descripcion], [DVH]) VALUES (4, N'Ciudadela', 944);" +
                                        "SET IDENTITY_INSERT[dbo].[Localidad] OFF";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }


        public void InsertarPerfilUsrCompleto()
        {
            const string sqlStatement = "DELETE FROM SEG_PerfilUsr; " +
                                        "INSERT [dbo].[SEG_PerfilUsr] ([Id], [Descripcion], [DVH]) VALUES (1, N'WebMaster', 955); " +
                                        "INSERT[dbo].[SEG_PerfilUsr] ([Id], [Descripcion], [DVH]) VALUES(2, N'Administrativo', 1536); " +
                                        "INSERT[dbo].[SEG_PerfilUsr] ([Id], [Descripcion], [DVH]) VALUES(3, N'Cliente', 759); " +
                                        "INSERT[dbo].[SEG_PerfilUsr] ([Id], [Descripcion], [DVH]) VALUES(4, N'Visita', 676);";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }


        public int ValidarExistencia(string tabla)
        {

            const string sqlStatement = "SELECT COUNT(*) FROM sysobjects WHERE type = 'U' AND name = @tabla";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@tabla", DbType.String, tabla);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
        }

        public int ExisteRegistroIntegridad(string Col_A, string Col_B, string Col_C)
        {

            const string sqlStatement = "SELECT COUNT(*) FROM SEG_IntegridadRegistros WHERE Col_A = @Col_A AND Col_B = @Col_B AND Col_C = @Col_C";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Col_A", DbType.String, Col_A);
                db.AddInParameter(cmd, "@Col_B", DbType.String, Col_B);
                db.AddInParameter(cmd, "@Col_C", DbType.String, Col_C);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
        }


        public int ExisteRegTablaDVV(string tabla)
        {

            const string sqlStatement = "SELECT COUNT(*) FROM SEG_DVV WHERE Tabla=@tabla";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@tabla", DbType.String, tabla);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
        }


        public int ValidarDVV(string tabla, long DVV)
        {
            string sqlStatement = "SELECT COUNT(*) FROM dbo.SEG_DVV WHERE [Tabla]=@tabla AND [DVV]=@DVV";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@tabla", DbType.String, tabla);
                db.AddInParameter(cmd, "@DVV", DbType.Int64, DVV);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public int ValidarCantidadReg(string tabla, int cantReg)
        {
            string sqlStatement = "SELECT COUNT(*) FROM dbo.SEG_DVV WHERE [Tabla]=@tabla AND [Valor]=@cantReg";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@tabla", DbType.String, tabla);
                db.AddInParameter(cmd, "@DVV", DbType.Int64, cantReg);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }


        public long CalcularDVV(string tabla)
        {
            string sqlStatement = "SELECT IsNull(SUM(DVH),0) FROM " + tabla;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public long ActualizarDVV(string tabla, long DVV, int CantReg)
        {
            const string sqlStatement = "UPDATE  dbo.SEG_DVV SET [DVV]=@DVV, [CantidadReg]=@CantReg WHERE [Tabla]=@tabla";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVV", DbType.Int64, DVV);
                db.AddInParameter(cmd, "@tabla", DbType.String, tabla);
                db.AddInParameter(cmd, "@CantReg", DbType.Int32, CantReg);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public int ContarRegistros(string tabla)
        {
            string sqlStatement = "SELECT COUNT(*) FROM " + tabla;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public void grabarRegistroIntegridad(string Col_A = "N/A", string Col_B = "N/A", string Col_C = "N/A", string Col_D = "N/A", string Col_E = "N/A", string Col_F = "N/A", string Col_G = "N/A")
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_IntegridadRegistros VALUES(@Col_A, @Col_B, @Col_C, @Col_D, @Col_E, @Col_F, @Col_G);";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Col_A", DbType.String, Col_A);
                db.AddInParameter(cmd, "@Col_B", DbType.String, Col_B);
                db.AddInParameter(cmd, "@Col_C", DbType.String, Col_C);
                db.AddInParameter(cmd, "@Col_D", DbType.String, Col_D);
                db.AddInParameter(cmd, "@Col_E", DbType.String, Col_E);
                db.AddInParameter(cmd, "@Col_F", DbType.String, Col_F);
                db.AddInParameter(cmd, "@Col_G", DbType.String, Col_G);

                db.ExecuteScalar(cmd);
            }

        }

        public void LimpiarTablaRegistrosTablasFaltantes()
        {
            string sqlStatement = "DELETE dbo.SEG_IntegridadRegistros;";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.ExecuteScalar(cmd);
            }

        }

        public List<IntegridadRegistros> ListarRegistrosTablasFaltantes()
        {
            const string sqlStatement = "SELECT [Col_A], [Col_B], [Col_C], [Col_D], [Col_E], [Col_F], [Col_G] FROM dbo.SEG_IntegridadRegistros";

            var result = new List<IntegridadRegistros>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var registro = MapearRegistros(dr); // Mapper
                        result.Add(registro);
                    }
                }
            }

            return result;

        }

        private static IntegridadRegistros MapearRegistros(IDataReader dr)
        {
            var registro = new IntegridadRegistros
            {
                Col_A = GetDataValue<string>(dr, "Col_A"),
                Col_B = GetDataValue<string>(dr, "Col_B"),
                Col_C = GetDataValue<string>(dr, "Col_C"),
                Col_D = GetDataValue<string>(dr, "Col_D"),
                Col_E = GetDataValue<string>(dr, "Col_E"),
                Col_F = GetDataValue<string>(dr, "Col_F"),
                Col_G = GetDataValue<string>(dr, "Col_G")

            };

            return registro;
        }

        public void ActualizarDVHUsuario(int id, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario SET [DVH]=@DVH WHERE Id=@id";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@id", DbType.Int32, id);

                db.ExecuteScalar(cmd);
            }

        }
        public void ActualizarDVHProducto(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Producto SET [DVH]=@DVH WHERE Codigo=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHCategoria(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Categoria SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHCliente(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Cliente SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHDetalleOperacion(int codope, int codprod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.DetalleOperacion SET [DVH]=@DVH WHERE OperacionId=@OperaId and ProductoId=@ProdId";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@OperaId", DbType.Int32, codope);
                db.AddInParameter(cmd, "@ProdId", DbType.Int32, codprod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHFactura(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Factura SET [DVH]=@DVH WHERE Codigo=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHFormaPago(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.FormaPago SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHEstadoOperacion(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.EstadoOperacion SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHIdioma(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Idioma SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHLocalidad(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Localidad SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHMarca(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Marca SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHOperacion(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Operacion SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHBitacora(long cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Bitacora SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHPerfilUsr(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.SEG_PerfilUsr SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHPermisos(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Permisos SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHDetallePermisos(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.SEG_DetallePermisos SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void ActualizarDVHStock(int cod, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.Stock SET [DVH]=@DVH WHERE Id=@cod";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@cod", DbType.Int32, cod);

                db.ExecuteScalar(cmd);
            }
        }

        public void RealizarBackUp()
        {
            const string sqlStatement = "DECLARE @path VARCHAR(500) DECLARE @name VARCHAR(500) DECLARE @pathwithname VARCHAR(500) DECLARE @time DATETIME " +
                                        "DECLARE @year VARCHAR(4) DECLARE @month VARCHAR(2) DECLARE @day VARCHAR(2) DECLARE @hour VARCHAR(2) " +
                                        "DECLARE @minute VARCHAR(2) DECLARE @second VARCHAR(2) " +

                                        //2.Definir la ruta del archivo.
                                        //"SET @path = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL12.SQLEXPRESS\\MSSQL\\Backup\\' " +
                                        "SET @path = 'C:\\Implantagraf\\RESPALDOS\\' " +
                                         
                                        //3.Setear las variables.
                                        "SELECT @time = GETDATE() SELECT @year = (SELECT CONVERT(VARCHAR(4), DATEPART(yy, @time))) SELECT @month = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(mm, @time), '00'))) " +
                                        "SELECT @day = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(dd, @time), '00'))) SELECT @hour = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(hh, @time), '00'))) " +
                                        "SELECT @minute = (SELECT CONVERT(VARCHAR(2), FORMAT(DATEPART(mi, @time), '00'))) " +

                                        //4.Defining the filename format
                                        "SELECT @name = 'Implantagraf' + '_' + @year + @month + @day + '_' + @hour + @minute SET @pathwithname = @path + @namE + '.bak' " +

                                        //5.Executing the backup command.
                                        "BACKUP DATABASE[Implantagraf] TO DISK = @pathwithname WITH NOFORMAT, NOINIT, SKIP, REWIND, NOUNLOAD, STATS = 10;";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.ExecuteScalar(cmd);
            }

        }

        public List<Respaldo> ListarRespaldos()
        {
            List<Respaldo> listaRespaldos = new List<Respaldo>();

            //System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo(@"C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\Backup");
            System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo(@"C:\\Implantagraf\\RESPALDOS");
            

        FileInfo[] archivos = directorio.GetFiles();

            foreach (var archivoActual in archivos)
            {
                var respaldoActual = new Respaldo { Ruta = archivoActual.FullName };
                listaRespaldos.Add(respaldoActual);

            }

            return listaRespaldos.OrderByDescending(x => x.Ruta).ToList();
        }

        public void RestaurarCopiaRespaldo(string rutaCompleta)
        {
            string sqlStatement = "USE[master] " +
                                  "ALTER DATABASE [Implantagraf] SET SINGLE_USER WITH ROLLBACK IMMEDIATE " +
                                  "ALTER DATABASE [Implantagraf] SET MULTI_USER " +
                                  "RESTORE DATABASE [Implantagraf] " +
                                  "FROM DISK = '" + rutaCompleta + "' WITH REPLACE;";

            var db = DatabaseFactory.CreateDatabase(ConnectionNameMaster);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                cmd.CommandTimeout = 600;
                db.ExecuteNonQuery(cmd);
            }
        }

    }
}