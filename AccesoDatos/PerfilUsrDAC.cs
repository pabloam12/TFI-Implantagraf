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
        public PerfilUsr Agregar(PerfilUsr perfilUsr, long DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_PerfilUsr ([Descripcion],[FechaAlta],[FechaBaja],[FechaModi], [DVH]) " +
                "VALUES(@Descripcion,@FechaAlta,@FechaBaja,@FechaModi,@DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, perfilUsr.Descripcion);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

                // Ejecuto la consulta y guardo el id que devuelve.
                perfilUsr.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return perfilUsr;

        }

        public void ActualizarPorId(PerfilUsr perfilUsr, long DVH)
        {
            const string sqlStatement = "UPDATE dbo.SEG_PerfilUsr " +
                "SET [Descripcion]=@Descripcion, [FechaModi]=@FechaModi, [DVH]=@DVH" +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, perfilUsr.Descripcion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, perfilUsr.Id);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

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
                
                db.ExecuteNonQuery(cmd);
            }
        }

        public PerfilUsr BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] " +
                "FROM dbo.SEG_PerfilUsr WHERE [ID]=@Id ";

            PerfilUsr perfilUsr = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) perfilUsr = MapearPerfilUsr(dr); // Mapper
                }
            }

            return perfilUsr;
        }

        public List<PerfilUsr> Listar()
        {

            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] FROM dbo.SEG_PerfilUsr ORDER BY [Descripcion]";

            var result = new List<PerfilUsr>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var perfilUsr = MapearPerfilUsr(dr); // Mapper
                        result.Add(perfilUsr);
                    }
                }
            }

            return result;
        }


        private static PerfilUsr MapearPerfilUsr(IDataReader dr)
        {
            var perfilUsr = new PerfilUsr
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };
            return perfilUsr;
        }
    }
}