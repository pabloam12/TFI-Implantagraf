using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class OperacionesDAC : DataAccessComponent

    {
        public Factura RegistrarFactura(Factura factura, long DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.Factura  ([FechaHora], [Tipo], [Monto], [FormaPagoId], [Direccion], [RazonSocial], [Email], [DVH])" +
            "VALUES(@FechaHora, @Tipo, @Monto, @FormaPagoId, @Direccion, @RazonSocial, @Email, @DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, factura.FechaHora);
                db.AddInParameter(cmd, "@Tipo", DbType.String, factura.Tipo);
                db.AddInParameter(cmd, "@Monto", DbType.Int64, factura.Monto);
                db.AddInParameter(cmd, "@FormaPagoId", DbType.Int32, factura.FormaPagoId);
                db.AddInParameter(cmd, "@Direccion", DbType.String, factura.Direccion);
                db.AddInParameter(cmd, "@RazonSocial", DbType.String, factura.RazonSocial);
                db.AddInParameter(cmd, "@Email", DbType.String, factura.Email);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

                // Ejecuto la consulta y guardo el id que devuelve.
                factura.Codigo = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return factura;

        }

    }
}