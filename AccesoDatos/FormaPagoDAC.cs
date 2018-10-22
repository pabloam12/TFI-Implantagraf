using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Entidades;

namespace AccesoDatos
{
    public class FormaPagoDAC : DataAccessComponent

    {
        public List<FormaPago> Listar()
        {

            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] FROM dbo.Categoria  ORDER BY [Descripcion]";

            var result = new List<FormaPago>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var formaPago = MapearFormaPago(dr); // Mapper
                        result.Add(formaPago);
                    }
                }
            }

            return result;
        }

        private static FormaPago MapearFormaPago(IDataReader dr)
        {
            var formaPago = new FormaPago
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };
            return formaPago;
        }
    }
}