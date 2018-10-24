using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class Marca_TC_DAC : DataAccessComponent

    {        
        public List<Marca_TC> Listar()
        {

            const string sqlStatement = "SELECT [Id], [Descripcion] FROM dbo.WS_Marca_TC ORDER BY [Descripcion]";

            var result = new List<Marca_TC>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var marca_TC = MapearMarca_TC(dr); // Mapper
                        result.Add(marca_TC);
                    }
                }
            }

            return result;
        }


        private static Marca_TC MapearMarca_TC(IDataReader dr)
        {
            var marca_TC = new Marca_TC
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion")                

            };
            return marca_TC;
        }
    }
}