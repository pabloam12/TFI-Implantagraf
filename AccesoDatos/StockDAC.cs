using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Entidades;

namespace AccesoDatos
{
    public class StockDAC : DataAccessComponent

    {
        public List<Stock> VerStock()
        {

            const string sqlStatement = "SELECT [Id], [ProductoId], [FechaCalendario], [Cantidad], [TipoOperacionId], [DVH] FROM dbo.Stock  ORDER BY [FechaCalendario]";

            var result = new List<Stock>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var stock = MapearStock(dr); // Mapper
                        result.Add(stock);
                    }
                }
            }

            return result;
        }

        private static Stock MapearStock(IDataReader dr)
        {
            var stock = new Stock
            {
                Id = GetDataValue<int>(dr, "Id"),
                ProductoId = GetDataValue<Int32>(dr, "ProductoId"),
                FechaCalendario = GetDataValue<DateTime>(dr, "FechaCalendario"),
                Cantidad = GetDataValue<Int32>(dr, "Cantidad"),
                TipoOperacionId = GetDataValue<Int32>(dr, "TipoOperacionId"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };
            return stock;
        }
    }
}