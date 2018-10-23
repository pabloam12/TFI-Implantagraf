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
        //public Usuario BuscarPorId(int id)
        //{
        //    const string sqlStatement = "SELECT [Id], [RazonSocial], [CUIL], [Email], [Telefono], [Direccion], [LocalidadId], [FechaAlta] " +
        //        "FROM dbo.SEG_Usuario WHERE [Id]=@Id AND [PerfilId]='3'";

        //    Usuario cliente = null;

        //    var db = DatabaseFactory.CreateDatabase(ConnectionName);
        //    using (var cmd = db.GetSqlStringCommand(sqlStatement))
        //    {
        //        db.AddInParameter(cmd, "@Id", DbType.Int32, id);

        //        using (var dr = db.ExecuteReader(cmd))
        //        {
        //            if (dr.Read()) cliente = MapearCliente(dr); // Mapper
        //        }
        //    }

        //    return cliente;
        //}

        public List<Cliente> Listar()
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [CUIL], [Email], [Telefono], [Direccion], [LocalidadId], [FechaAlta], [DVH] "+
                "FROM dbo.Cliente ORDER BY [RazonSocial]";

            var result = new List<Cliente>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                
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


        private static Cliente MapearCliente(IDataReader dr)
        {
            var localidadDAC = new LocalidadDAC();
            var cliente = new Cliente
            {
                Id = GetDataValue<int>(dr, "Id"),
                RazonSocial = GetDataValue<string>(dr, "RazonSocial"),
                CUIL = GetDataValue<string>(dr, "CUIL"),
                Email = GetDataValue<string>(dr, "Email"),
                Telefono = GetDataValue<string>(dr, "Telefono"),
                Direccion = GetDataValue<string>(dr, "Direccion"),
                Localidad = localidadDAC.BuscarPorId(GetDataValue<int>(dr, "LocalidadId")), //Mapper
                FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };
            return cliente;
        }
    }
}