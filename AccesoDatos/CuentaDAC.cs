using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public List<Usuario> ListarClientesPorFiltro(string fecha, string fechaFin, string usr)
        {

            var sqlStatement = "SELECT [Id], [RazonSocial], [Nombre], [Apellido], [Usr], [Psw], [CUIL], [Estado], [Email], [Telefono], " +
               "[Direccion], [LocalidadId], [FechaAlta], [FechaBaja], [PerfilId], [IdiomaId], [DVH] " +
               "FROM dbo.SEG_Usuario ";

            var whereStatement = "";


            if (fecha != "")
            {
                fecha = InvertirFecha(fecha);

                whereStatement = "Where [FechaAlta] >= @fecha and [FechaAlta] < dateadd(day,1,@fecha) ";

                if (fechaFin != "")
                {

                    fechaFin = InvertirFecha(fechaFin);

                    whereStatement = "Where [FechaAlta] >= @fecha and [FechaAlta] < dateadd(day,1,@fechaFin) ";

                }
            }

            if (usr != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Usuario] like '%" + usr + "%' "; }

                else { whereStatement = whereStatement + "AND [Usuario] like '%" + usr + "%' "; }

            }
                                    
            sqlStatement = sqlStatement + whereStatement + "ORDER BY [FechaAlta];";


            var result = new List<Usuario>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@fecha", DbType.String, fecha);
                db.AddInParameter(cmd, "@fechaFin", DbType.String, fechaFin);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var usrActual = MapearUsuario(dr); // Mapper
                        result.Add(usrActual);
                    }
                }
            }

            return result;
        }

        public Usuario BuscarUsuarioPorUsuario(string usr)
        {
            usr = CifrarTripleDES(usr);

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


        public bool ValidarUsuario(String nombreUsuario)
        {
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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

            //Cifrar Datos
            usuarioModif.Direccion = CifrarTripleDES(usuarioModif.Direccion);
            usuarioModif.Telefono = CifrarTripleDES(usuarioModif.Telefono);
            
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
            nombreUsuario = CifrarTripleDES(nombreUsuario);
            PswUsuario = CifrarTripleDES(PswUsuario);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            usr = CifrarTripleDES(usr);
            nuevaPsw = CifrarTripleDES(nuevaPsw);

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
            nombreUsuario = CifrarTripleDES(nombreUsuario);

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
            usr.Usuario = CifrarTripleDES(usr.Usuario);
            usr.Contraseña = CifrarTripleDES(usr.Contraseña);

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
            //Cifrar Datos
            usr.Psw = CifrarTripleDES(usr.Psw);
            usr.Usr = CifrarTripleDES(usr.Usr);
            usr.RazonSocial = CifrarTripleDES(usr.RazonSocial);
            usr.Nombre = CifrarTripleDES(usr.Nombre);
            usr.Apellido = CifrarTripleDES(usr.Apellido);
            usr.Direccion = CifrarTripleDES(usr.Direccion);
            usr.CUIL = CifrarTripleDES(usr.CUIL);
            usr.Telefono = CifrarTripleDES(usr.Telefono);
            usr.Email = CifrarTripleDES(usr.Email);

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

            //Cifrar Datos
            usr.Psw = DescifrarTripleDES(usr.Psw);
            usr.Usr = DescifrarTripleDES(usr.Usr);
            usr.RazonSocial = DescifrarTripleDES(usr.RazonSocial);
            usr.Nombre = DescifrarTripleDES(usr.Nombre);
            usr.Apellido = DescifrarTripleDES(usr.Apellido);
            usr.Direccion = DescifrarTripleDES(usr.Direccion);
            usr.CUIL = DescifrarTripleDES(usr.CUIL);
            usr.Telefono = DescifrarTripleDES(usr.Telefono);
            usr.Email = DescifrarTripleDES(usr.Email);

            return usr;

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


        public List<PermisosUsr> ListarPermisos()
        {
            const string sqlStatement = "SELECT Id, Descripcion, DVH " +
                                        "FROM [dbo].[SEG_Permisos];";

            var result = new List<PermisosUsr>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var permiso = MapearPermisoUsr(dr); // Mapper
                        result.Add(permiso);
                    }
                }
            }

            return result;

        }

        public List<DetallePermisoUsr> ListarDetallePermisos()
        {
            const string sqlStatement = "SELECT DP.Id, DP.UsrId, DP.PermisoId, P.Descripcion, DP.Otorgado, DP.DVH " +
                                        "FROM [dbo].[SEG_DetallePermisos] as DP " +
                                        "JOIN [dbo].[SEG_Permisos] as P " +
                                            "ON P.Id = DP.PermisoId ";

            var result = new List<DetallePermisoUsr>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var detalle = MapearDetallePermisoUsr(dr); // Mapper
                        result.Add(detalle);
                    }
                }
            }

            return result;

        }

        public List<DetallePermisoUsr> ListarDetallePermisosPorUsuario(int usuarioId)
        {
            const string sqlStatement = "SELECT DP.Id, DP.UsrId, DP.PermisoId, P.Descripcion, DP.Otorgado, DP.DVH " +
                                        "FROM [dbo].[SEG_DetallePermisos] as DP " +
                                        "JOIN [dbo].[SEG_Permisos] as P " +
                                            "ON P.Id = DP.PermisoId " +
                                        "WHERE DP.UsrId = @UsuarioId; ";

            var result = new List<DetallePermisoUsr>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@UsuarioId", DbType.Int32, usuarioId);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var detalle = MapearDetallePermisoUsr(dr); // Mapper
                        result.Add(detalle);
                    }
                }
            }

            return result;

        }

        public void OtorgarPermisosWebmaster(int usrId)
        {

            const string sqlStatement = "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,1,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,2,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,3,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,4,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,5,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,6,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,7,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,8,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,9,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,10,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,11,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,12,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,13,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,14,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,15,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,16,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,17,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,18,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,19,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,20,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,21,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,22,'N',0); SELECT SCOPE_IDENTITY();";


            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@UsrId", DbType.Int32, usrId);


                Convert.ToInt32(db.ExecuteScalar(cmd));

            }

        }

        public void OtorgarPermisosCliente(int usrId)
        {

            const string sqlStatement = "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,1,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,2,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,3,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,4,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,5,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,6,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,7,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,8,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,9,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,10,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,11,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,12,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,13,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,14,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,15,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,16,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,17,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,18,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,19,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,20,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,21,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,22,'S',0); SELECT SCOPE_IDENTITY();";


            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@UsrId", DbType.Int32, usrId);


                Convert.ToInt32(db.ExecuteScalar(cmd));

            }

        }

        public void OtorgarPermisosAdministrativo(int usrId)
        {

            const string sqlStatement = "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,1,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,2,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,3,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,4,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,5,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,6,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,7,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,8,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,9,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,10,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,11,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,12,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,13,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,14,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,15,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,16,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,17,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,18,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,19,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,20,'N',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,21,'S',0); SELECT SCOPE_IDENTITY();" +
                                        "INSERT [dbo].[SEG_DetallePermisos] ([UsrId], [PermisoId], [Otorgado], [DVH]) VALUES (@UsrId,22,'N',0); SELECT SCOPE_IDENTITY();";


            var db = DatabaseFactory.CreateDatabase(ConnectionName);


            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@UsrId", DbType.Int32, usrId);


                Convert.ToInt32(db.ExecuteScalar(cmd));

            }

        }


        public void ActualizarPermiso(int detallePermisoId, string otorgamiento)
        {

            const string sqlStatement = "UPDATE dbo.SEG_DetallePermisos " +
                "SET[Otorgado] = @otorgamiento WHERE [Id] = @id";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@id", DbType.Int32, detallePermisoId);
                db.AddInParameter(cmd, "@otorgamiento", DbType.String, otorgamiento);

                db.ExecuteNonQuery(cmd);
            }

        }

        public DetallePermisoUsr BuscarDetallePermisoPorId(int detallePermisoId)
        {

            const string sqlStatement = "SELECT DP.Id, DP.UsrId, DP.PermisoId, P.Descripcion, DP.Otorgado, DP.DVH " +
                                        "FROM [dbo].[SEG_DetallePermisos] as DP " +
                                        "JOIN [dbo].[SEG_Permisos] as P " +
                                            "ON P.Id = DP.PermisoId " +
                                        "WHERE DP.Id = @detallePermisoId; ";

            var result = new DetallePermisoUsr();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@detallePermisoId", DbType.Int32, detallePermisoId);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        result = MapearDetallePermisoUsr(dr); // Mapper
                        
                    }
                }
            }

            return result;

        }    



        private PermisosUsr MapearPermisoUsr(IDataReader dr)
        {

            var permiso = new PermisosUsr
            {
                Id = GetDataValue<int>(dr, "Id"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                DVH = GetDataValue<Int64>(dr, "DVH")
            };

            return permiso;
        }

        private DetallePermisoUsr MapearDetallePermisoUsr(IDataReader dr)
        {

            var detallePermiso = new DetallePermisoUsr
            {
                Id = GetDataValue<int>(dr, "Id"),
                UsrId = GetDataValue<int>(dr, "UsrId"),
                PermisoId = GetDataValue<int>(dr, "PermisoId"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                Otorgado = GetDataValue<string>(dr, "Otorgado"),
                DVH = GetDataValue<Int64>(dr, "DVH")
            };

            return detallePermiso;
        }

        private Usuario MapearUsuario(IDataReader dr)
        {
            var localidadDAC = new LocalidadDAC();
            var perfilUsrDAC = new PerfilUsrDAC();
            var idiomaDAC = new IdiomaDAC();
            
            var usuario = new Usuario
            {
                Id = GetDataValue<int>(dr, "Id"),
                RazonSocial = DescifrarTripleDES(GetDataValue<string>(dr, "RazonSocial")),
                Nombre = DescifrarTripleDES(GetDataValue<string>(dr, "Nombre")),
                Apellido = DescifrarTripleDES(GetDataValue<string>(dr, "Apellido")),
                Usr = DescifrarTripleDES(GetDataValue<string>(dr, "Usr")),
                Psw = DescifrarTripleDES(GetDataValue<string>(dr, "Psw")),
                CUIL = DescifrarTripleDES(GetDataValue<string>(dr, "CUIL")),
                Estado = GetDataValue<string>(dr, "Estado"),
                Email = DescifrarTripleDES(GetDataValue<string>(dr, "Email")),
                Telefono = DescifrarTripleDES(GetDataValue<string>(dr, "Telefono")),
                Direccion = DescifrarTripleDES(GetDataValue<string>(dr, "Direccion")),
                Localidad = localidadDAC.BuscarPorId(GetDataValue<int>(dr, "LocalidadId")), //Mapper
                PerfilUsr = perfilUsrDAC.BuscarPorId(GetDataValue<int>(dr, "PerfilId")), //Mapper
                Idioma = idiomaDAC.BuscarPorId(GetDataValue<int>(dr, "IdiomaId")), //Mapper
                FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta"),
                FechaBaja = GetDataValue<DateTime>(dr, "FechaBaja"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };

            return usuario;
        }

        private string InvertirFecha(string fecha)
        {

            var anio = fecha.Substring(0, 4);
            var mes = fecha.Substring(5, 2);
            var dia = fecha.Substring(8, 2);

            return (dia + "-" + mes + "-" + anio);

        }

        public string clave = "TrabajoFinalTFI";

        private string CifrarTripleDES(string cadena)
        {

            byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

            byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();

            return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
        }
        private string DescifrarTripleDES(string cadena)
        {

            byte[] llave;

            byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateDecryptor();
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
            tripledes.Clear();

            string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
            return cadena_descifrada; // Devolvemos la cadena
        }
    }
}

