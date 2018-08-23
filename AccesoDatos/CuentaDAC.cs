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
        public Usuario Registrar(Usuario usr, Int64 DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Usuario ([RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], " +
                "[Estado], [Intentos], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH]) " +

                "VALUES(@RazonSocial, @Nombre, @Apellido, @Usr, @Psw, @CUIL, " +
                "@Estado, @Intentos, @Email, @Telefono, " +
                "@Direccion, @LocalidadId, @FechaNacimiento, @FechaAlta, @PerfilId, @IdiomaId, @DVH); SELECT SCOPE_IDENTITY(); ";
                
            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@RazonSocial", DbType.String, usr.RazonSocial);
                db.AddInParameter(cmd, "@Nombre", DbType.String, usr.RazonSocial);
                db.AddInParameter(cmd, "@Apellido", DbType.String, usr.RazonSocial);
                db.AddInParameter(cmd, "@Usr", DbType.String, usr.Email);
                db.AddInParameter(cmd, "@Psw", DbType.String, usr.Psw);
                db.AddInParameter(cmd, "@CUIL", DbType.String, usr.CUIL);
                db.AddInParameter(cmd, "@Estado", DbType.String, 'S');
                db.AddInParameter(cmd, "@Intentos", DbType.Int32, 0);
                db.AddInParameter(cmd, "@Email", DbType.String, usr.Email);
                db.AddInParameter(cmd, "@Telefono", DbType.String, usr.Telefono);
                db.AddInParameter(cmd, "@Direccion", DbType.String, usr.Direccion);
                db.AddInParameter(cmd, "@LocalidadId", DbType.Int32, usr.Localidad.Id);
                db.AddInParameter(cmd, "@FechaNacimiento", DbType.Date, usr.FechaNacimiento);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@PerfilId", DbType.Int32, 4);
                db.AddInParameter(cmd, "@IdiomaId", DbType.Int32, usr.Idioma.Id);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

                // Ejecuto la consulta y guardo el id que devuelve.
                usr.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return usr;


        }
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

        private Usuario MapearUsuario(IDataReader dr)
        {
            var localidadDAC = new LocalidadDAC();
            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();

            var usuario = new Usuario
            {
                Id = GetDataValue<int>(dr, "ID"),
                Nombre = GetDataValue<string>(dr, "Nombre"),
                Apellido = GetDataValue<string>(dr, "Apellido"),
                CUIL = GetDataValue<string>(dr, "CUIL"),
                Email = GetDataValue<string>(dr, "Email"),
                Telefono = GetDataValue<string>(dr, "Telefono"),
                Direccion = GetDataValue<string>(dr, "Direccion"),
                Localidad = localidadDAC.ListarPorId(GetDataValue<int>(dr, "LocalidadId")), //Mapper
                FechaNacimiento = GetDataValue<DateTime>(dr, "FechaNacimiento"),
                FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta"),
                Perfil = perfilUsrDAC.ListarPorId(GetDataValue<int>(dr, "PerfilId")), //Mapper
                Idioma = idiomaDAC.ListarPorId(GetDataValue<int>(dr, "IdiomaId")) //Mapper
            };

            return usuario;
        }

    }
}