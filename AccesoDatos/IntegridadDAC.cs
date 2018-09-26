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

        public long CalcularDVV(string tabla)
        {
            string sqlStatement = "SELECT SUM(DVH) FROM " + tabla;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

        public long ActualizarDVV(string tabla, long DVV)
        {
            const string sqlStatement = "UPDATE  dbo.SEG_DVV SET [Valor]=@DVV WHERE [Tabla]=@tabla";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@DVV", DbType.Int64, DVV);
                db.AddInParameter(cmd, "@tabla", DbType.String, tabla);

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
    }
}