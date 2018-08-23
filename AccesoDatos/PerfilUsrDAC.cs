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
            const string sqlStatement = "INSERT INTO dbo.SEG_PerfilUsr ([Descripcion]) " +
                "VALUES(@Descripcion); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, perfilUsr.Descripcion);

                // Ejecuto la consulta y guardo el id que devuelve.
                perfilUsr.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return perfilUsr;

        }

        public void ActualizarPorId(PerfilUsr perfilUsr)
        {
            const string sqlStatement = "UPDATE dbo.SEG_PerfilUsr " +
                "SET [Descripcion]=@Descripcion " +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, perfilUsr.Descripcion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, perfilUsr.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "DELETE FROM dbo.SEG_PerfilUsr WHERE [ID]=@Id ";
            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);

                db.ExecuteNonQuery(cmd);
            }
        }

        public PerfilUsr ListarPorId(int id)
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

            const string sqlStatement = "SELECT [ID], [Descripcion] FROM dbo.SEG_PerfilUsr ORDER BY [Descripcion]";

            var result = new List<PerfilUsr>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var category = CargarPerfilUsr(dr); // Mapper
                        result.Add(category);
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