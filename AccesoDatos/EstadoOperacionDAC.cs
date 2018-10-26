using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class EstadoOperacionDAC : DataAccessComponent

    {
       
        public EstadoOperacion BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] " +
                "FROM dbo.EstadoOperacion WHERE [ID]=@Id ";

            EstadoOperacion estadoOperacion = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) estadoOperacion = MapearEstadoOperacion(dr); // Mapper
                }
            }

            return estadoOperacion;
        }

        public List<EstadoOperacion> Listar()
        {

            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] FROM dbo.EstadoOperacion ORDER BY [Descripcion]";

            var result = new List<EstadoOperacion>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var estadoOperacion = MapearEstadoOperacion(dr); // Mapper
                        result.Add(estadoOperacion);
                    }
                }
            }

            return result;
        }


        private static EstadoOperacion MapearEstadoOperacion(IDataReader dr)
        {
            var estadoOperacion = new EstadoOperacion
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };
            return estadoOperacion;
        }
    }
}