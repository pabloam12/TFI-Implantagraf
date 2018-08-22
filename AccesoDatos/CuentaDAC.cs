using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccesoDatos
{
    public class CuentaDAC : DataAccessComponent
    {

        public Usuario Autenticar(Usuario usr)
        {
            const string sqlStatement = "SELECT [ID], [Nombre], [Apellido], [CUIL], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId]  " +
                "FROM dbo.SEG_Usuario WHERE [Usr]=@Usr AND [Psw]=@Psw ";

            Usuario usuario = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, usr.Usr);
                db.AddInParameter(cmd, "@Psw", DbType.String, usr.Psw);

                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) usuario = MapearUsuario(dr); // Mapper
                }
            }

            return usuario;
        }

        private static Usuario MapearUsuario(IDataReader dr)
        {
            var usuario = new Usuario
            {
                //Id = GetDataValue<int>(dr, "ID"),
                //Nombre = GetDataValue<string>(dr, "Nombre"),
                //Apellido = GetDataValue<string>(dr, "Apellido"),
                //CUIL = GetDataValue<string>(dr, "CUIL"),
                //Email = GetDataValue<string>(dr, "Email"),
                //Telefono = GetDataValue<string>(dr, "Telefono"),
                //Direccion = GetDataValue<string>(dr, "Direccion"),
                //LocalidadId = GetDataValue<int>(dr, "LocalidadId"),
                //FechaNacimiento = GetDataValue<DateTime>(dr, "FechaNacimiento"),
                //FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta"),
                //PerfilId = GetDataValue<int>(dr, "PerfilId"),
                //IdiomaId = GetDataValue<int>(dr, "IdiomaId")
            };

            return usuario;
        }

    }
}