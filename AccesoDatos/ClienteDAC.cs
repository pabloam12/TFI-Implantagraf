using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class ClienteDAC : DataAccessComponent

    {
        public Usuario BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [RazonSocial], [CUIL], [Email], [Telefono], [Direccion], [LocalidadId], [FechaAlta] " +
                "FROM dbo.SEG_Usuario WHERE [Id]=@Id AND [PerfilId]='3'";

            Usuario cliente = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);

                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) cliente = MapearCliente(dr); // Mapper
                }
            }

            return cliente;
        }

        public List<Usuario> Listar()
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [CUIL], [Email], [Telefono], [Direccion], [LocalidadId], [FechaAlta] FROM dbo.SEG_Usuario WHERE [PerfilId]=@PerfilId AND (FechaBaja = 2000/01/01 OR FechaBaja is null) ORDER BY [RazonSocial]";

            var result = new List<Usuario>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@PerfilId", DbType.Int32, 3);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var cliente = MapearCliente(dr); // Mapper
                        result.Add(cliente);
                    }
                }
            }

            return result;
        }


        private static Usuario MapearCliente(IDataReader dr)
        {
            var localidadDAC = new LocalidadDAC();
            var cliente = new Usuario
            {
                Id = GetDataValue<int>(dr, "ID"),
                RazonSocial = GetDataValue<string>(dr, "RazonSocial"),
                CUIL = GetDataValue<string>(dr, "CUIL"),
                Email = GetDataValue<string>(dr, "Email"),
                Telefono = GetDataValue<string>(dr, "Telefono"),
                Direccion = GetDataValue<string>(dr, "Direccion"),
                Localidad = localidadDAC.BuscarPorId(GetDataValue<int>(dr, "LocalidadId")), //Mapper
                FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta")

            };
            return cliente;
        }
    }
}