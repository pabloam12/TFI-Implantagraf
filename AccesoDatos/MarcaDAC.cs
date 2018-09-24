using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccesoDatos
{
    public class MarcaDAC : DataAccessComponent

    {
        public Marca Agregar(Marca marca)
        {
            const string sqlStatement = "INSERT INTO dbo.Marca ([Descripcion],[FechaAlta],[FechaBaja],[FechaModi]) " +
                "VALUES(@Descripcion,@FechaAlta,@FechaBaja,@FechaModi); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, marca.Descripcion);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, new DateTime(2000, 01, 01));
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, new DateTime(2000, 01, 01));

                // Ejecuto la consulta y guardo el id que devuelve.

                marca.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return marca;
        }

        public void ActualizarPorId(Marca marca)
        {
            const string sqlStatement = "UPDATE dbo.Marca " +
                "SET [Descripcion]=@Descripcion , [FEchaModi]=@FechaModi" +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, marca.Descripcion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, marca.Id);
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }


        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "UPDATE dbo.Marca " +
                  "SET [FEchaBaja]=@FechaBaja" +
                  "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
              
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }
        }

        public Marca BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion] " +
                "FROM dbo.Marca WHERE [ID]=@Id ";

            Marca marca = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) marca = MapearMarca(dr); // Mapper
                }
            }

            return marca;
        }
	
        public List<Marca> Listar()
        {

            const string sqlStatement = "SELECT [ID], [Descripcion] FROM dbo.Marca ORDER BY [Descripcion]";

            var result = new List<Marca>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var marca = MapearMarca(dr); // Mapper
                        result.Add(marca);
                    }
                }
            }

            return result;
        }

        private static Marca MapearMarca(IDataReader dr)
        {
            var marca = new Marca
            {
                Id = GetDataValue<int>(dr, "ID"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),

            };
            return marca;
        }
    }
}