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

        public List<Usuario> ListarUsuarios()
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Estado], [Email], [Telefono], " +
               "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja], [PerfilId], [IdiomaId], [DVH] " +
               "FROM dbo.SEG_Usuario;";

            var result = new List<Usuario>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

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

        public List<Usuario> ListarUsuariosPorPerfil(int perfil)
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Estado], [Email], [Telefono], " +
               "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja], [PerfilId], [IdiomaId], [DVH] " +
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

        public Usuario BuscarUsuarioPorUsuario(string usr)
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Estado], [Email], [Telefono], " +
               "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja], [PerfilId], [IdiomaId], [DVH] " +
               "FROM dbo.SEG_Usuario WHERE [Usr]=@usr;";

            Usuario usuario = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@usr", DbType.String, usr);

                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) usuario = MapearUsuario(dr); // Mapper
                }
            }

            return usuario;
        }
        public Usuario ListarUsuarioPorId(int codUsuario)
        {

            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Estado], [Email], [Telefono], " +
               "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja], [PerfilId], [IdiomaId], [DVH] " +
               "FROM dbo.SEG_Usuario WHERE [Id]=@codUsuario;";

            Usuario usuario = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@codUsuario", DbType.Int32, codUsuario);

                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) usuario = MapearUsuario(dr); // Mapper
                }
            }

            return usuario;
        }

        public List<PermisosUsr> VerPermisosUsuario()
        {
            const string sqlStatement = "SELECT P.Id, P.Descripcion, P.DVH " +
                                        "FROM [dbo].[SEG_Permisos] as P " +
                                        "JOIN [dbo].[SEG_DetallePermisos] as DP " +
                                            "ON P.Id = DP.PermisoId " +
                                        "JOIN [dbo].[SEG_PerfilUsr] as PF " +
                                            "ON PF.Id = DP.PerfilId " +
                                        "JOIN [dbo].[SEG_Usuario] as U " +
                                            "ON U.PerfilId = DP.PerfilId; ";

            var result = new List<PermisosUsr>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var permiso = MapearPermisoUsuario(dr); // Mapper
                        result.Add(permiso);
                    }
                }
            }

            return result;

        }

        public List<PermisosUsr> VerPermisosUsuario(int usuarioId)
        {
            const string sqlStatement = "SELECT P.Id, P.Descripcion, P.DVH " +
                                        "FROM [dbo].[SEG_Permisos] as P " +
                                        "JOIN [dbo].[SEG_DetallePermisos] as DP " +
                                            "ON P.Id = DP.PermisoId " +
                                        "JOIN [dbo].[SEG_PerfilUsr] as PF " +
                                            "ON PF.Id = DP.PerfilId " +
                                        "JOIN [dbo].[SEG_Usuario] as U " +
                                            "ON U.PerfilId = DP.PerfilId " +
                                        "WHERE U.Id = @UsuarioId; ";

            var result = new List<PermisosUsr>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@UsuarioId", DbType.Int32, usuarioId);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var permiso = MapearPermisoUsuario(dr); // Mapper
                        result.Add(permiso);
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

        public void BloquearCuenta(int usuarioId)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Estado] = 'B' WHERE [Id] = @Id";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, usuarioId);

                db.ExecuteNonQuery(cmd);
            }

        }

        public void DesbloquearCuenta(int usuarioId)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Estado] = 'S' WHERE [Id] = @Id";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, usuarioId);

                db.ExecuteNonQuery(cmd);
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

        public void ActualizarPswUsuario(string usr, string nuevaPsw)
        {
            const string sqlStatement = "UPDATE dbo.SEG_Usuario " +
                "SET[Psw] = @nuevaPsw WHERE [Usr] = @usr";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@nuevaPsw", DbType.String, nuevaPsw);
                db.AddInParameter(cmd, "@usr", DbType.String, usr);

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

        public Usuario Autenticar(FrmLogin usr)
        {
            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Estado], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja],[PerfilId], [IdiomaId], [DVH]  " +
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
        public Usuario RegistrarUsuario(Usuario usr)
        {
            const string sqlStatement = "INSERT INTO dbo.SEG_Usuario ([RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], " +
                "[Estado], [Intentos], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja], [PerfilId], [IdiomaId], [DVH]) " +

                "VALUES(@RazonSocial, @Nombre, @Apellido, @Usr, @Psw, @CUIL, " +
                "@Estado, @Intentos, @Email, @Telefono, " +
                "@Direccion, @LocalidadId, @FechaAlta, @FechaBaja, @PerfilId, @IdiomaId, @DVH); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();
            var localidadDAC = new LocalidadDAC();

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@RazonSocial", DbType.String, usr.RazonSocial);
                db.AddInParameter(cmd, "@Nombre", DbType.String, usr.Nombre);
                db.AddInParameter(cmd, "@Apellido", DbType.String, usr.Apellido);
                db.AddInParameter(cmd, "@Usr", DbType.String, usr.Usr);
                db.AddInParameter(cmd, "@Psw", DbType.String, usr.Psw);
                db.AddInParameter(cmd, "@CUIL", DbType.String, usr.CUIL);
                db.AddInParameter(cmd, "@Estado", DbType.String, usr.Estado);
                db.AddInParameter(cmd, "@Intentos", DbType.Int32, 0);
                db.AddInParameter(cmd, "@Email", DbType.String, usr.Email);
                db.AddInParameter(cmd, "@Telefono", DbType.String, usr.Telefono);
                db.AddInParameter(cmd, "@Direccion", DbType.String, usr.Direccion);
                db.AddInParameter(cmd, "@LocalidadId", DbType.Int32, usr.Localidad.Id);
                db.AddInParameter(cmd, "@FechaAlta", DbType.DateTime, usr.FechaAlta);
                db.AddInParameter(cmd, "@PerfilId", DbType.Int32, usr.PerfilUsr.Id);
                db.AddInParameter(cmd, "@IdiomaId", DbType.Int32, usr.Idioma.Id);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, 0);
                db.AddInParameter(cmd, "@FechaBaja", DbType.DateTime, usr.FechaBaja);

                // Ejecuto la consulta y guardo el id que devuelve.
                usr.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));

            
                    usr.PerfilUsr = perfilUsrDAC.BuscarPorId(usr.PerfilUsr.Id); // Mapper
                    usr.Idioma = idiomaDAC.BuscarPorId(usr.Idioma.Id); // Mapper
                    usr.Localidad = localidadDAC.BuscarPorId(usr.Localidad.Id); // Mapper
                
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
                RazonSocial = GetDataValue<string>(dr, "RazonSocial"),
                Nombre = GetDataValue<string>(dr, "Nombre"),
                Apellido = GetDataValue<string>(dr, "Apellido"),
                Usr = GetDataValue<string>(dr, "Usr"),
                Psw = GetDataValue<string>(dr, "Psw"),
                CUIL = GetDataValue<string>(dr, "CUIL"),
                Estado = GetDataValue<string>(dr, "Estado"),
                Email = GetDataValue<string>(dr, "Email"),
                Telefono = GetDataValue<string>(dr, "Telefono"),
                Direccion = GetDataValue<string>(dr, "Direccion"),
                Localidad = localidadDAC.BuscarPorId(GetDataValue<int>(dr, "LocalidadId")), //Mapper
                PerfilUsr = perfilUsrDAC.BuscarPorId(GetDataValue<int>(dr, "PerfilId")), //Mapper
                Idioma = idiomaDAC.BuscarPorId(GetDataValue<int>(dr, "IdiomaId")), //Mapper
                FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta"),
                FechaBaja = GetDataValue<DateTime>(dr, "FechaBaja"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };

            return usuario;
        }


        public Usuario InformacionCuenta(string idUsuario)
        {
            const string sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Estado], [Email], [Telefono], " +
                "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja], [PerfilId], [IdiomaId], [DVH] " +
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

        private PermisosUsr MapearPermisoUsuario(IDataReader dr)
        {

            var permiso = new PermisosUsr
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                DVH = GetDataValue<Int64>(dr, "DVH")
            };

            return permiso;
        }
    }
}