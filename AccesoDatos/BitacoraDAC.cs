using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccesoDatos
{
    public class BitacoraDAC : DataAccessComponent
    {
        public bool grabarBitacora(DateTime fechaHora, String usuario, String descripcion, String criticidad, long DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Bitacora ([FechaHora], [Usuario], [Descripcion], [Criticidad], [DVH]) " +

                "VALUES(@FechaHora, @Usuario, @Descripcion, @Criticidad, @DVH ); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, fechaHora);
                db.AddInParameter(cmd, "@Usuario", DbType.String, usuario);
                db.AddInParameter(cmd, "@Descripcion", DbType.String, descripcion);
                db.AddInParameter(cmd, "@Criticidad", DbType.String, criticidad);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

                // Ejecuto la consulta y devuelve si inserto o no.
                return (Convert.ToBoolean(db.ExecuteScalar(cmd)));
                
            }

      }


    }
    
}