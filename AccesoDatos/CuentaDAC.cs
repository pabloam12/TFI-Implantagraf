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

        public List<Usuario> ListarUsuariosPorPerfil(int perfil)
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Email], [Telefono], " +
               "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH] " +
               "FROM dbo.SEG_Usuario WHERE [PerfilId]=@PerfilId;";

            var result = new List<Usuario>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@PerfilId", DbType.Int32, perfil);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var usuario = MapearUsuario(dr); // Mapper
                        result.Add(usuario);
                    }
                }
            }

            return result;
        }

        public bool ValidarUsuario(String nombreUsuario)
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

        public bool ValidarSesionActiva(String nombreUsuario)
        {
            const string sqlStatement = "SELECT COUNT(*) FROM dbo.SEG_Usuario WHERE [Usr]=@Usr AND [Estado]='A'";

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

        public void ActualizarDatosCuenta(Usuario usuarioModif)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET [Direccion]=@Direccion, [LocalidadId]=@Localidad, [Telefono]=@Telefono, [IdiomaId]=@IdiomaId " +
                "WHERE [Id]=@Id";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, usuarioModif.Id);

                db.AddInParameter(cmd, "@Direccion", DbType.String, usuarioModif.Direccion);
                db.AddInParameter(cmd, "@Localidad", DbType.String, usuarioModif.Localidad.Id);
                db.AddInParameter(cmd, "@Telefono", DbType.String, usuarioModif.Telefono);
                db.AddInParameter(cmd, "@IdiomaId", DbType.Int32, usuarioModif.Idioma.Id);

                db.ExecuteNonQuery(cmd);
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
                "SET[Intentos] += 1 WHERE [Usr] = @Usr AND [PerfilId] <> 1";

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

        public void ActivarSesionCuentaUsuario(String nombreUsuario)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Estado] = 'A' WHERE [Usr] = @Usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);

                db.ExecuteNonQuery(cmd);
            }

        }

        public void ActivarCuentaUsuario(String nombreUsuario)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Estado] = 'S' WHERE [Usr] = @Usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, nombreUsuario);

                db.ExecuteNonQuery(cmd);
            }

        }

        public Usuario Autenticar(Login usr)
        {
            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH]  " +
                "FROM dbo.SEG_Usuario WHERE [Usr]=@Usr AND [Psw]=@Psw ";

            Usuario usuario = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Usr", DbType.String, usr.Usuario);
                db.AddInParameter(cmd, "@Psw", DbType.String, usr.Contraseña);

                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) usuario = MapearUsuario(dr); // Mapper
                }
            }

            //Reinicia los intentos fallidos.
            ReiniciarIntentosFallidos(usuario.Usr);

            return usuario;
        }
        public Usuario RegistrarCliente(Usuario usr, Int64 DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Usuario ([RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], " +
                "[Estado], [Intentos], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [FechaBaja], [FechaModi], [PerfilId], [IdiomaId], [DVH]) " +

                "VALUES(@RazonSocial, @Nombre, @Apellido, @Usr, @Psw, @CUIL, " +
                "@Estado, @Intentos, @Email, @Telefono, " +
                "@Direccion, @LocalidadId, @FechaAlta, @FechaAlta, @FechaBaja, @FechaModi, @PerfilId, @IdiomaId, @DVH); SELECT SCOPE_IDENTITY(); ";

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
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@PerfilId", DbType.Int32, 3);
                db.AddInParameter(cmd, "@IdiomaId", DbType.Int32, 1);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, new DateTime(2000, 01, 01));
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, new DateTime(2000, 01, 01));

                // Ejecuto la consulta y guardo el id que devuelve.
                usr.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));

                usr.Nombre = usr.RazonSocial;
                usr.Apellido = usr.RazonSocial;
                usr.Usr = usr.Email;

                usr.PerfilUsr = perfilUsrDAC.BuscarPorId(3); // Mapper
                usr.Idioma = idiomaDAC.BuscarPorId(1); // Mapper
                usr.Localidad = localidadDAC.BuscarPorId(usr.Localidad.Id); // Mapper
            }

            return usr;

        }

        public void RegistrarUsuario(Usuario usr, int perfil, int idioma, int localidad, Int64 DVH)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Usuario ([RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], " +
                "[Estado], [Intentos], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH], [FechaBaja], [FechaModi]) " +

                "VALUES(@RazonSocial, @Nombre, @Apellido, @Usr, @Psw, @CUIL, " +
                "@Estado, @Intentos, @Email, @Telefono, " +
                "@Direccion, @LocalidadId, @FechaNacimiento, @FechaAlta, @PerfilId, @IdiomaId, @DVH, @FechaBaja, @FechaModi); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();
            var localidadDAC = new LocalidadDAC();

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@RazonSocial", DbType.String, usr.Nombre + usr.Apellido);
                db.AddInParameter(cmd, "@Nombre", DbType.String, usr.Nombre);
                db.AddInParameter(cmd, "@Apellido", DbType.String, usr.Apellido);
                db.AddInParameter(cmd, "@Usr", DbType.String, usr.Usr);
                db.AddInParameter(cmd, "@Psw", DbType.String, usr.Psw);
                db.AddInParameter(cmd, "@CUIL", DbType.String, usr.CUIL);
                db.AddInParameter(cmd, "@Estado", DbType.String, 'S');
                db.AddInParameter(cmd, "@Intentos", DbType.Int32, 0);
                db.AddInParameter(cmd, "@Email", DbType.String, usr.Email);
                db.AddInParameter(cmd, "@Telefono", DbType.String, usr.Telefono);
                db.AddInParameter(cmd, "@Direccion", DbType.String, usr.Direccion);
                db.AddInParameter(cmd, "@LocalidadId", DbType.Int32, localidad);
                db.AddInParameter(cmd, "@FechaNacimiento", DbType.Date, usr.FechaNacimiento);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@PerfilId", DbType.Int32, perfil);
                db.AddInParameter(cmd, "@IdiomaId", DbType.Int32, idioma);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, new DateTime(2000, 01, 01));
                db.AddInParameter(cmd, "@FechaModi", DbType.DateTime, new DateTime(2000, 01, 01));

                // Ejecuto la consulta y guardo el id que devuelve.
                usr.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));

            }

        }

        private Usuario MapearUsuario(IDataReader dr)
        {
            var localidadDAC = new LocalidadDAC();
            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();

            var usuario = new Usuario
            {
                Id = GetDataValue<int>(dr, "Id"),
                RazonSocial = GetDataValue<string>(dr, "RazonSocial"),
                Nombre = GetDataValue<string>(dr, "Nombre"),
                Apellido = GetDataValue<string>(dr, "Apellido"),
                Usr = GetDataValue<string>(dr, "Usr"),
                Psw = GetDataValue<string>(dr, "Psw"),
                PswConfirmacion = GetDataValue<string>(dr, "Psw"),
                CUIL = GetDataValue<string>(dr, "CUIL"),
                Email = GetDataValue<string>(dr, "Email"),
                Telefono = GetDataValue<string>(dr, "Telefono"),
                Direccion = GetDataValue<string>(dr, "Direccion"),
                Localidad = localidadDAC.BuscarPorId(GetDataValue<int>(dr, "LocalidadId")), //Mapper
                FechaNacimiento = GetDataValue<DateTime>(dr, "FechaNacimiento"),
                PerfilUsr = perfilUsrDAC.BuscarPorId(GetDataValue<int>(dr, "PerfilId")), //Mapper
                Idioma = idiomaDAC.BuscarPorId(GetDataValue<int>(dr, "IdiomaId")), //Mapper
                FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta"),
                DVH = GetDataValue<Int64>(dr, "DVH")
            };

            return usuario;
        }

        public Usuario InformacionCuenta(string idUsuario)
        {
            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH] " +
                "FROM dbo.SEG_Usuario WHERE [Id]=@idUsuario";

            var infoUsuario = new Usuario();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@idUsuario", DbType.String, idUsuario);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        infoUsuario = MapearUsuario(dr); // Mapper

                    }
                }
            }

            return infoUsuario;
        }
    }
}