using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class WS_DAC : DataAccessComponent

    {

        public long ValidarTarjeta(string numeroTC, int marcaId, string mesVenc, string anioVenc, string codSeguridad)
        {
            const string sqlStatement = "SELECT IsNull([Limite],0) " +
                "FROM dbo.WS_Empresa_TC " +
                "WHERE [NroTarjeta]=@numeroTC AND [MarcaId]=@marcaId AND [MesVenc]=@mesVenc AND [AnioVenc]=@anioVenc AND [CodSeguridad]=@codSeguridad;";
            
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@numeroTC", DbType.String, numeroTC);
                db.AddInParameter(cmd, "@marcaId", DbType.Int32, marcaId);
                db.AddInParameter(cmd, "@mesVenc", DbType.String, mesVenc);
                db.AddInParameter(cmd, "@anioVenc", DbType.String, anioVenc);
                db.AddInParameter(cmd, "@codSeguridad", DbType.String, codSeguridad);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }

        }

    }
}