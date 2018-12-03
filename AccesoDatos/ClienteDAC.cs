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
    public class ClienteDAC : DataAccessComponent

    {
        public Cliente RegistrarCliente(Usuario usuario)
        {
            usuario.RazonSocial = CifrarTripleDES(usuario.RazonSocial);
            usuario.CUIL = CifrarTripleDES(usuario.CUIL);
            usuario.Direccion = CifrarTripleDES(usuario.Direccion);
            usuario.Telefono = CifrarTripleDES(usuario.Telefono);
            usuario.Email = CifrarTripleDES(usuario.Email);

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
                    RazonSocial = DescifrarTripleDES(usuario.RazonSocial),
                    CUIL = DescifrarTripleDES(usuario.CUIL),
                    Email = DescifrarTripleDES(usuario.Email),
                    Telefono = DescifrarTripleDES(usuario.Telefono),
                    Direccion = DescifrarTripleDES(usuario.Direccion),
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


        private Cliente MapearCliente(IDataReader dr)
        {
            var localidadDAC = new LocalidadDAC();
            var cliente = new Cliente
            {
                Id = GetDataValue<int>(dr, "Id"),
                RazonSocial = DescifrarTripleDES(GetDataValue<string>(dr, "RazonSocial")),
                CUIL = DescifrarTripleDES(GetDataValue<string>(dr, "CUIL")),
                Email = DescifrarTripleDES(GetDataValue<string>(dr, "Email")),
                Telefono = DescifrarTripleDES(GetDataValue<string>(dr, "Telefono")),
                Direccion = DescifrarTripleDES(GetDataValue<string>(dr, "Direccion")),
                Localidad = localidadDAC.BuscarPorId(GetDataValue<int>(dr, "LocalidadId")), //Mapper
                FechaAlta = GetDataValue<DateTime>(dr, "FechaAlta"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };

            return cliente;
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