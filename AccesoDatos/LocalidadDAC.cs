using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class LocalidadDAC : DataAccessComponent

    {
        public Localidad Agregar(Localidad localidad)
        {
            const string sqlStatement = "INSERT INTO dbo.Localidad  ([Descripcion]) " +
                "VALUES(@Descripcion); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, localidad.Descripcion);
                

                // Ejecuto la consulta y guardo el id que devuelve.
                localidad.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return localidad;

        }

        public void ActualizarPorId(Localidad localidad)
        {
            const string sqlStatement = "UPDATE dbo.Localidad " +
                "SET [Descripcion]=@Descripcion " +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, localidad.Descripcion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, localidad.Id);
             

                db.ExecuteNonQuery(cmd);
            }
        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "UPDATE dbo.Localidad " +
               "SET [FechaBaja]=@FechaBaja " +
               "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }
        }

        public Localidad BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] " +
                "FROM dbo.Localidad WHERE [ID]=@Id ";

            Localidad localidad = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) localidad = MapearLocalidad(dr); // Mapper
                }
            }

            return localidad;
        }

        public List<Localidad> Listar()
        {

            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] FROM dbo.Localidad ORDER BY [Descripcion]";

            var result = new List<Localidad>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var localidad = MapearLocalidad(dr); // Mapper
                        result.Add(localidad);
                    }
                }
            }

            return result;
        }


        private static Localidad MapearLocalidad(IDataReader dr)
        {
            var localidad = new Localidad
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };
            return localidad;
        }
    }
}