using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class PerfilUsrDAC : DataAccessComponent

    {
        public PerfilUsr Agregar(PerfilUsr perfilUsr)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_PerfilUsr ([Descripcion],[FechaAlta],[FechaBaja],[FechaModi]) " +
                "VALUES(@Descripcion,@FechaAlta,@FechaBaja,@FechaModi); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, perfilUsr.Descripcion);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, new DateTime(2000, 01, 01));
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, new DateTime(2000, 01, 01));

                // Ejecuto la consulta y guardo el id que devuelve.
                perfilUsr.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return perfilUsr;

        }

        public void ActualizarPorId(PerfilUsr perfilUsr)
        {
            const string sqlStatement = "UPDATE dbo.SEG_PerfilUsr " +
                "SET [Descripcion]=@Descripcion, [FechaModi]=@FechaModi" +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, perfilUsr.Descripcion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, perfilUsr.Id);
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }
        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "UPDATE dbo.SEG_PerfilUsr " +
                "SET [Descripcion]=@Descripcion, [FechaBaja]=@FechaBaja" +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }
        }

        public PerfilUsr BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion] " +
                "FROM dbo.SEG_PerfilUsr WHERE [ID]=@Id ";

            PerfilUsr perfilUsr = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) perfilUsr = CargarPerfilUsr(dr); // Mapper
                }
            }

            return perfilUsr;
        }

        public List<PerfilUsr> Listar()
        {

            const string sqlStatement = "SELECT [ID], [Descripcion] FROM dbo.SEG_PerfilUsr WHERE FechaBaja = 2000/01/01 OR FechaBaja is null ORDER BY [Descripcion]";

            var result = new List<PerfilUsr>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var perfilUsr = CargarPerfilUsr(dr); // Mapper
                        result.Add(perfilUsr);
                    }
                }
            }

            return result;
        }


        private static PerfilUsr CargarPerfilUsr(IDataReader dr)
        {
            var perfilUsr = new PerfilUsr
            {
                Id = GetDataValue<int>(dr, "ID"),
                Descripcion = GetDataValue<string>(dr, "Descripcion")

            };
            return perfilUsr;
        }
    }
}