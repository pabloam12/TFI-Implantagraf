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

        public bool ValidarUsuario (String nombreUsuario)
        {
            const string sqlStatement = "SELECT COUNT(*) FROM dbo.SEG_Usuario WHERE [Usr]=@Usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
           

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);

                if (Convert.ToInt32(db.ExecuteScalar(cmd)) != 0)
                    { return false; }

                // Si no existe el usuario devuelve true asi entra en el if.
                return true;
            }

        }

        public bool ValidarBloqueoCuenta(String nombreUsuario)
        {
            const string sqlStatement = "SELECT COUNT(*) FROM dbo.SEG_Usuario WHERE [Usr]=@Usr AND [Estado]='B'";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);

                if (Convert.ToInt32(db.ExecuteScalar(cmd)) == 0)
                { return false; }

                // Si la cuenta esta bloqueada devuelve true asi entra en el if.
                return true;
            }

        }

        public bool ValidarUsuarioPsw(String nombreUsuario, String PswUsuario)
        {
            const string sqlStatement = "SELECT COUNT(*) FROM dbo.SEG_Usuario WHERE [Usr]=@Usr AND [Psw]=@Psw";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);
                db.AddInParameter(cmd, "@Psw", DbType.String, PswUsuario);

                // No hay coincidencias.
                if (Convert.ToInt32(db.ExecuteScalar(cmd)) == 0)
                { return true; }

                // Si la contraseña esta bien devuelve true asi no entra en el if.
                return false;
            }

        }

        public void ReiniciarIntentosFallidos(string nombreUsuario)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Intentos] = 0 WHERE [Usr] = @Usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);

                db.ExecuteNonQuery(cmd);
            }

        }

        public int SumarIntentoFallido(String nombreUsuario)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Intentos] += 1 WHERE [Usr] = @Usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);
                
                db.ExecuteNonQuery(cmd);
            }

            return CantidadIntentos(nombreUsuario);
        }

        public int CantidadIntentos(string nombreUsuario)
        {
            const string sqlStatement = "SELECT [Intentos] from dbo.SEG_Usuario WHERE [Usr] = @Usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);

                return Convert.ToInt32(db.ExecuteScalar(cmd));
            };
        }

        public void BloquearCuentaUsuario(String nombreUsuario)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Estado] = 'B' WHERE [Usr] = @Usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);

                db.ExecuteNonQuery(cmd);
            }

        }

        public Usuario Autenticar(Usuario usr)
        {
            const string sqlStatement = "SELECT [Id], [Nombre], [Apellido], [CUIL], [Email], [Telefono], " +
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
        public Usuario RegistrarCliente(Usuario usr, Int64 DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Usuario ([RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], " +
                "[Estado], [Intentos], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH]) " +

                "VALUES(@RazonSocial, @Nombre, @Apellido, @Usr, @Psw, @CUIL, " +
                "@Estado, @Intentos, @Email, @Telefono, " +
                "@Direccion, @LocalidadId, @FechaNacimiento, @FechaAlta, @PerfilId, @IdiomaId, @DVH); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();
            var localidadDAC = new LocalidadDAC();

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

                usr.Nombre = usr.RazonSocial;
                usr.Apellido = usr.RazonSocial;
                usr.Usr = usr.Email;

                usr.Perfil = perfilUsrDAC.ListarPorId(4); // Mapper
                usr.Idioma = idiomaDAC.ListarPorId(usr.Idioma.Id); // Mapper
                usr.Localidad = localidadDAC.ListarPorId(usr.Localidad.Id); // Mapper
            }

            return usr;

        }
        
        private Usuario MapearUsuario(IDataReader dr)
        {
            var localidadDAC = new LocalidadDAC();
            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();

            var usuario = new Usuario
            {
                Id = GetDataValue<int>(dr, "Id"),
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




        //TODO
        public List<Informacion> informacionCuenta(int id)
        {
            const string sqlStatement = "SELECT [RazonSocial], [Telefono], " +
                "[Direccion]  " +
                "FROM dbo.SEG_Usuario WHERE [Id]=@Id";

            var result = new List<Informacion>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var category = CargarLista(dr); // Mapper
                        result.Add(category);
                    }
                }
            }

            return result;

        }

        //TODO
        private static Informacion CargarLista(IDataReader dr)
        {
            var informacion = new Informacion
            {
                RazonSocial = GetDataValue<string>(dr, "RazonSocial"),
                Direccion = GetDataValue<string>(dr, "Direccion"),
                Telefono = GetDataValue<string>(dr, "Telefono")

            };
            return informacion;
        }

    }
}