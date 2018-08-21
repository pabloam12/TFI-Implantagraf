using AccesoDatos.ImplantagrafDataModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccesoDatos
{
    public class LocalidadDAC : DataAccessComponent


    {
        public void Agregar(Localidad localidad)
        {

            const string sqlStatement = "INSERT INTO dbo.Localidad ([Descripcion] " +
                "VALUES(@Descripcion, ); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Name", DbType.String, category.Name);
                db.AddInParameter(cmd, "@CreatedOn", DbType.DateTime2, category.CreatedOn);
                db.AddInParameter(cmd, "@CreatedBy", DbType.Int32, category.CreatedBy);
                db.AddInParameter(cmd, "@ChangedOn", DbType.DateTime2, category.ChangedOn);
                db.AddInParameter(cmd, "@ChangedBy", DbType.Int32, category.ChangedBy);
                // Obtener el valor de la primary key.
                category.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return category;
        }

        public void Listar()
        {

            var context = new ImplantagrafModelEntities();

            context.Localidad.All();
        }

    }
        
}