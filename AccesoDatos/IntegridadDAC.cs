using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
            string sqlStatement = "SELECT COUNT(*) FROM dbo.SEG_DVV WHERE [Tabla]=@tabla AND [Valor]=@DVV";

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
            string sqlStatement = "SELECT SUM(DVH) FROM " + tabla;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public long ActualizarDVV(string tabla, long DVV, int CantReg)
        {
            const string sqlStatement = "UPDATE  dbo.SEG_DVV SET [Valor]=@DVV, [CantidadReg]=@CantReg WHERE [Tabla]=@tabla";

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

    }
}