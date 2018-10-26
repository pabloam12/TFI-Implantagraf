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
        public Cliente RegistrarCliente(Usuario usuario)
        {
            const string sqlStatement = "INSERT INTO dbo.Cliente ([Id], [RazonSocial], [CUIL], [Email], [Telefono], [Direccion], [LocalidadId], [FechaAlta], [DVH]) " +
                                        "VALUES(@Id, @RazonSocial, @CUIL, @Email, @Telefono, @Direccion, @LocalidadId, @FechaAlta, @DVH);";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();
            var localidadDAC = new LocalidadDAC();
            var fechaHora = DateTime.Now;
                       

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, usuario.Id);
                db.AddInParameter(cmd, "@RazonSocial", DbType.String, usuario.RazonSocial);
                db.AddInParameter(cmd, "@CUIL", DbType.String, usuario.CUIL);
                db.AddInParameter(cmd, "@Email", DbType.String, usuario.Email);
                db.AddInParameter(cmd, "@Telefono", DbType.String, usuario.Telefono);
                db.AddInParameter(cmd, "@Direccion", DbType.String, usuario.Direccion);
                db.AddInParameter(cmd, "@LocalidadId", DbType.Int32, usuario.Localidad.Id);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, fechaHora);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, 0);

                // Ejecuto la consulta.
                db.ExecuteScalar(cmd);

                var cliente = new Cliente
                {
                    Id = usuario.Id,
                    RazonSocial = usuario.RazonSocial,
                    CUIL = usuario.CUIL,
                    Email = usuario.Email,
                    Telefono = usuario.Telefono,
                    Direccion = usuario.Direccion,
                    Localidad = localidadDAC.BuscarPorId(usuario.Localidad.Id), //Mapper
                    FechaAlta = fechaHora,
                    DVH = 0
                };

                return cliente;
            }
                       

        }

        public Cliente BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [RazonSocial], [CUIL], [Email], [Telefono], [Direccion], [LocalidadId], [FechaAlta], [DVH] " +
                "FROM dbo.Cliente WHERE [Id]=@Id";

            Cliente cliente = null;

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

        public List<Cliente> Listar()
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [CUIL], [Email], [Telefono], [Direccion], [LocalidadId], [FechaAlta], [DVH] " +
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

        public int ExisteCliente(int codUsuario)
        {

            const string sqlStatement = "SELECT COUNT(*) FROM Cliente WHERE [Id] = @codUsuario";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@codUsuario", DbType.Int32, codUsuario);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
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