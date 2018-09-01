using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class IdiomaDAC : DataAccessComponent

    {
        public Idioma Agregar(Idioma idioma)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Idioma ([Descripcion], {Abreviacion]) " +
                "VALUES(@Descripcion, @Abreviacion); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, idioma.Descripcion);
                db.AddInParameter(cmd, "@Abreviacion", DbType.String, idioma.Abreviacion);

                // Ejecuto la consulta y guardo el id que devuelve.
                idioma.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return idioma;

        }

        public void ActualizarPorId(Idioma idioma)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Idioma " +
                "SET [Descripcion]=@Descripcion, [Abreviacion]=@Abreviacion " +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, idioma.Descripcion);
                db.AddInParameter(cmd, "@Abreviacion", DbType.String, idioma.Abreviacion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, idioma.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "DELETE FROM dbo.SEG_Idioma WHERE [ID]=@Id ";
            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);

                db.ExecuteNonQuery(cmd);
            }
        }

        public Idioma ListarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion], [Abreviacion] " +
                "FROM dbo.SEG_Idioma WHERE [ID]=@Id ";

            Idioma idioma = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) idioma = CargarIdioma(dr); // Mapper
                }
            }

            return idioma;
        }

        public List<Idioma> Listar()
        {

            const string sqlStatement = "SELECT [ID], [Descripcion], [Abreviacion] FROM dbo.SEG_Idioma ORDER BY [Descripcion]";

            var result = new List<Idioma>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var idioma = CargarIdioma(dr); // Mapper
                        result.Add(idioma);
                    }
                }
            }

            return result;
        }


        private static Idioma CargarIdioma(IDataReader dr)
        {
            var idioma = new Idioma
            {
                Id = GetDataValue<int>(dr, "ID"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                Abreviacion = GetDataValue<string>(dr, "Abreviacion")

            };
            return idioma;
        }
    }
}