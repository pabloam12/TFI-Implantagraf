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
        public void ActualizarDVHProducto (int cod, long DVH)
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
                                        "SET @path = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL12.SQLEXPRESS\\MSSQL\\Backup\\' " +

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

            System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo(@"C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\Backup");

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