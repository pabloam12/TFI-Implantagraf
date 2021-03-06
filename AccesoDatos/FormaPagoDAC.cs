﻿using Microsoft.Practices.EnterpriseLibrary.Data;
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

        public FormaPago BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Descripcion], [DVH] " +
                "FROM dbo.FormaPago WHERE [ID]=@Id ";

            FormaPago formaPago = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) formaPago = MapearFormaPago(dr); // Mapper
                }
            }

            return formaPago;
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