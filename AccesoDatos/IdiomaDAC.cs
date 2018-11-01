using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class IdiomaDAC : DataAccessComponent

    {
        public Idioma Agregar(Idioma idioma, long DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.Idioma ([Descripcion], [Abreviacion], [FechaAlta], [FechaBaja], [FechaModi], [DVH]) " +
                "VALUES(@Descripcion, @Abreviacion,@FechaAlta,@FechaBaja,@FechaModi,@DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, idioma.Descripcion);
                db.AddInParameter(cmd, "@Abreviacion", DbType.String, idioma.Abreviacion);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, new DateTime(2000, 01, 01));
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, new DateTime(2000, 01, 01));
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

                // Ejecuto la consulta y guardo el id que devuelve.
                idioma.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return idioma;

        }

        public void ActualizarPorId(Idioma idioma)
        {
            const string sqlStatement = "UPDATE dbo.Idioma " +
                "SET [Descripcion]=@Descripcion, [Abreviacion]=@Abreviacion, [FechaModi]=@FechaModi " +
                "WHERE [ID]=@Id ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Descripcion", DbType.String, idioma.Descripcion);
                db.AddInParameter(cmd, "@Abreviacion", DbType.String, idioma.Abreviacion);
                db.AddInParameter(cmd, "@Id", DbType.Int32, idioma.Id);
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, DateTime.Now);

                db.ExecuteNonQuery(cmd);
            }
        }

        public void BorrarPorId(int id)
        {
            const string sqlStatement = "UPDATE dbo.Idioma " +
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

        public Idioma BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion], [Abreviacion], [DVH] " +
                "FROM dbo.Idioma WHERE [ID]=@Id ";

            Idioma idioma = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) idioma = MapearIdioma(dr); // Mapper
                }
            }

            return idioma;
        }

        public List<Idioma> Listar()
        {

            const string sqlStatement = "SELECT [ID], [Descripcion], [Abreviacion], [DVH] FROM dbo.Idioma ORDER BY [Descripcion]";

            var result = new List<Idioma>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var idioma = MapearIdioma(dr); // Mapper
                        result.Add(idioma);
                    }
                }
            }

            return result;
        }

        public Hashtable Traducir(string idioma)
        {

            string sqlStatement = "SELECT [Elemento], [" + idioma + "] as Traduccion FROM dbo.Traductor ORDER BY [Elemento]";

            var result = new Hashtable();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                //db.AddInParameter(cmd, "@Idioma", DbType.String, idioma);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var traduccion = MapearTraduccion(dr); // Mapper
                        result.Add(traduccion.Elemento, traduccion.Traduccion);
                    }
                }
            }

            return result;
        }



        private static Idioma MapearIdioma(IDataReader dr)
        {
            var idioma = new Idioma
            {
                Id = GetDataValue<int>(dr, "ID"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                Abreviacion = GetDataValue<string>(dr, "Abreviacion"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };
            return idioma;
        }

        private static Diccionario MapearTraduccion(IDataReader dr)
        {
            var diccionario = new Diccionario
            {
                Elemento = GetDataValue<string>(dr, "Elemento"),
                Traduccion = GetDataValue<string>(dr, "Traduccion")

            };
            return diccionario;
        }
    }
}