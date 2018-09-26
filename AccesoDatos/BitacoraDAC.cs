using Entidades;
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
        public List<Bitacora> ConsultarBitacora()
        {

            const string sqlStatement = "SELECT [Id], [FechaHora], [Usuario], [Accion], [Criticidad], [Detalle], [DVH] FROM dbo.SEG_Bitacora ORDER BY [FechaHora] DESC";


            var result = new List<Bitacora>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var bitacora = MapearBitacora(dr); // Mapper
                        result.Add(bitacora);
                    }
                }
            }

            return result;
        }

        public List<Bitacora> ConsultarBitacora(string fecha, string fechaFin, string usr, string accion, string criticidad)
        {

            var sqlStatement = "SELECT [Id], [FechaHora], [Usuario], [Accion], [Criticidad], [Detalle], [DVH] FROM dbo.SEG_Bitacora ";

            var whereStatement = "";

            if (fecha != "")
            {
                whereStatement = "Where [FechaHora] >= @fecha and [FechaHora] < dateadd(day,1,@fecha)  ";

                if (fechaFin != "")
                {
                    whereStatement = "Where [FechaHora] >= @fecha and [FechaHora] < dateadd(day,1,@fechaFin)  ";

                }
            }

            if (usr != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Usuario] like '%" + usr + "%' "; }

                else { whereStatement = whereStatement + "AND [Usuario] like '%" + usr + "%' "; }

            }

            if (accion != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Accion] like '%" + accion + "%' "; }

                else { whereStatement = whereStatement + "AND [Accion] like '%" + accion + "%' "; }

            }

            if (criticidad != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Criticidad] like '%" + criticidad + "%' "; }

                else { whereStatement = whereStatement + "AND [Criticidad] like '%" + criticidad + "%' "; }

            }

            sqlStatement = sqlStatement + whereStatement + "ORDER BY [FechaHora] DESC;";


            var result = new List<Bitacora>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@fecha", DbType.String, fecha);
                db.AddInParameter(cmd, "@fechaFin", DbType.String, fechaFin);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var bitacora = MapearBitacora(dr); // Mapper
                        result.Add(bitacora);
                    }
                }
            }

            return result;
        }


        public bool grabarBitacora(DateTime fechaHora, String usuario, String accion, String criticidad, String detalle, long DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Bitacora ([FechaHora], [Usuario], [Accion], [Criticidad], [Detalle], [DVH]) " +

                "VALUES(@FechaHora, @Usuario, @Descripcion, @Criticidad, @Detalle, @DVH ); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, fechaHora);
                db.AddInParameter(cmd, "@Usuario", DbType.String, usuario);
                db.AddInParameter(cmd, "@Descripcion", DbType.String, accion);
                db.AddInParameter(cmd, "@Criticidad", DbType.String, criticidad);
                db.AddInParameter(cmd, "@Detalle", DbType.String, detalle);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

                // Ejecuto la consulta y devuelve si inserto o no.
                return (Convert.ToBoolean(db.ExecuteScalar(cmd)));

            }

        }

        private static Bitacora MapearBitacora(IDataReader dr)
        {
            var bitacora = new Bitacora
            {
                Id = GetDataValue<Int64>(dr, "Id"),
                FechaHora = GetDataValue<DateTime>(dr, "FechaHora"),
                Usuario = GetDataValue<string>(dr, "Usuario"),
                Accion = GetDataValue<string>(dr, "Accion"),
                Criticidad = GetDataValue<string>(dr, "Criticidad"),
                Detalle = GetDataValue<string>(dr, "Detalle"),
                DVH = GetDataValue<Int64>(dr, "DVH"),

            };
            return bitacora;
        }


    }

}