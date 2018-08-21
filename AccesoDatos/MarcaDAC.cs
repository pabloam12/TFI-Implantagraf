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
            const string sqlStatement = "INSERT INTO dbo.Marca ([Descripcion]) " +
                "VALUES(@Descripcion); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, marca.Descripcion);

                db.ExecuteScalar(cmd);

                // Ejecuto la consulta y guardo el id que devuelve.

               marca.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return marca;
        }

        

        public void ActualizarPorId(Marca marca)
        {
            const string sqlStatement = "UPDATE dbo.Marca " +
                "SET [Descripcion]=@Descripcion " +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, marca.Descripcion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, marca.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "DELETE FROM dbo.Marca WHERE [ID]=@Id ";
            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);

                db.ExecuteNonQuery(cmd);
            }
        }

        public Marca ListarPorId(int id)
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
                    if (dr.Read()) marca = CargarMarca(dr); // Mapper
                }
            }

            return marca;
        }
	
        public List<Marca> Listar()
        {

            const string sqlStatement = "SELECT [ID], [Descripcion] FROM dbo.Marca ";

            var result = new List<Marca>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var marca = CargarMarca(dr); // Mapper
                        result.Add(marca);
                    }
                }
            }

            return result;
        }


        private static Marca CargarMarca(IDataReader dr)
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