using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Entidades;

namespace AccesoDatos
{
    public class CategoriaDAC : DataAccessComponent

    {
        public Categoria Agregar(Categoria categoria)
        {
            const string sqlStatement = "INSERT INTO dbo.Categoria ([Descripcion],[FechaAlta],[FechaBaja],[FechaModi]) " +
                "VALUES(@Descripcion,@FechaAlta,@FechaBaja,@FechaModi); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, categoria.Descripcion);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, new DateTime(2000, 01, 01));
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, new DateTime(2000, 01, 01));

                // Ejecuto la consulta y guardo el id que devuelve.

                categoria.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return categoria;
        }

        public void ActualizarPorId(Categoria categoria)
        {
            const string sqlStatement = "UPDATE dbo.Categoria " +
                "SET [Descripcion]=@Descripcion, [FechaModi]=@FechaModi " +
                "WHERE [Id]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, categoria.Descripcion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, categoria.Id);
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }
        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "UPDATE dbo.Categoria " +
                 "SET [FechaBaja]=@FechaBaja " +
                 "WHERE [Id]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }
        }

        public Categoria BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion] " +
                "FROM dbo.Categoria WHERE [Id]=@Id ";

            Categoria categoria = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) categoria = MapearCategoria(dr); // Mapper
                }
            }

            return categoria;
        }
	
        public List<Categoria> Listar()
        {

            const string sqlStatement = "SELECT [Id], [Descripcion] FROM dbo.Categoria  WHERE FechaBaja = 2000/01/01 OR FechaBaja is null ORDER BY [Descripcion]";

            var result = new List<Categoria>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var categoria = MapearCategoria(dr); // Mapper
                        result.Add(categoria);
                    }
                }
            }

            return result;
        }

        private static Categoria MapearCategoria(IDataReader dr)
        {
            var categoria = new Categoria
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),

            };
            return categoria;
        }
    }
}