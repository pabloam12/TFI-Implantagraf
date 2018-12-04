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
    public class BitacoraDAC : DataAccessComponent
    {
        public List<Bitacora> ConsultarBitacora()
        {

            const string sqlStatement = "SELECT [Id], [FechaHora], [Usuario], [Accion], [Criticidad], [Detalle], [DVH] "+
                "FROM dbo.SEG_Bitacora Where [Historico]=0 ORDER BY [FechaHora] ASC";


            var result = new List<Bitacora>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var bitacora = MapearBitacora(dr); // Mapper
                        result.Add(bitacora);
                    }
                }
            }

            return result;
        }

        public List<Bitacora> ConsultarBitacora(string fecha, string fechaFin, string usr, string accion, string criticidad)
        {

            var sqlStatement = "SELECT [Id], [FechaHora], [Usuario], [Accion], [Criticidad], [Detalle], [DVH] FROM dbo.SEG_Bitacora ";

            var whereStatement = "";
                   

            if (fecha != "")
            {
                fecha = InvertirFecha(fecha);

                whereStatement = "Where [FechaHora] >= @fecha and [FechaHora] < dateadd(day,1,@fecha) ";

                if (fechaFin != "")
                {

                    fechaFin = InvertirFecha(fechaFin);

                    whereStatement = "Where [FechaHora] >= @fecha and [FechaHora] < dateadd(day,1,@fechaFin) ";

                }
            }

            if (usr != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Usuario] like '%" + usr + "%' "; }

                else { whereStatement = whereStatement + "AND [Usuario] like '%" + usr + "%' "; }

            }

            if (accion != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Accion] like '%" + accion + "%' "; }

                else { whereStatement = whereStatement + "AND [Accion] like '%" + accion + "%' "; }

            }

            if (criticidad != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Criticidad] like '%" + criticidad + "%' "; }

                else { whereStatement = whereStatement + "AND [Criticidad] like '%" + criticidad + "%' "; }

            }

            if (whereStatement == "")

            { whereStatement = "Where [Historico] = 0 "; }

            else { whereStatement = whereStatement + "AND [Historico] = 0 "; }


            sqlStatement = sqlStatement + whereStatement + "ORDER BY [FechaHora] ASC;";


            var result = new List<Bitacora>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@fecha", DbType.String, fecha);
                db.AddInParameter(cmd, "@fechaFin", DbType.String, fechaFin);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var bitacora = MapearBitacora(dr); // Mapper
                        result.Add(bitacora);
                    }
                }
            }

            return result;
        }

        public List<Bitacora> ConsultarBitacoraHistorica(string fecha, string fechaFin, string usr, string accion, string criticidad)
        {

            var sqlStatement = "SELECT [Id], [FechaHora], [Usuario], [Accion], [Criticidad], [Detalle], [DVH] FROM dbo.SEG_Bitacora ";

            var whereStatement = "";


            if (fecha != "")
            {
                fecha = InvertirFecha(fecha);

                whereStatement = "Where [FechaHora] >= @fecha and [FechaHora] < dateadd(day,1,@fecha) ";

                if (fechaFin != "")
                {

                    fechaFin = InvertirFecha(fechaFin);

                    whereStatement = "Where [FechaHora] >= @fecha and [FechaHora] < dateadd(day,1,@fechaFin) ";

                }
            }

            if (usr != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Usuario] like '%" + usr + "%' "; }

                else { whereStatement = whereStatement + "AND [Usuario] like '%" + usr + "%' "; }

            }

            if (accion != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Accion] like '%" + accion + "%' "; }

                else { whereStatement = whereStatement + "AND [Accion] like '%" + accion + "%' "; }

            }

            if (criticidad != "")
            {
                if (whereStatement == "")

                { whereStatement = "Where [Criticidad] like '%" + criticidad + "%' "; }

                else { whereStatement = whereStatement + "AND [Criticidad] like '%" + criticidad + "%' "; }

            }

            sqlStatement = sqlStatement + whereStatement + "ORDER BY [FechaHora] DESC;";


            var result = new List<Bitacora>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@fecha", DbType.String, fecha);
                db.AddInParameter(cmd, "@fechaFin", DbType.String, fechaFin);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var bitacora = MapearBitacora(dr); // Mapper
                        result.Add(bitacora);
                    }
                }
            }

            return result;
        }


        public bool grabarBitacora(DateTime fechaHora, String usuario, String accion, String criticidad, String detalle, long DVH)
        {

            usuario = CifrarTripleDES(usuario);
            accion = CifrarTripleDES(accion);
            criticidad = CifrarTripleDES(criticidad);
            detalle = CifrarTripleDES(detalle);

            const string sqlStatement = "INSERT INTO dbo.SEG_Bitacora ([FechaHora], [Usuario], [Accion], [Criticidad], [Detalle], [DVH], [Historico]) " +

                "VALUES(@FechaHora, @Usuario, @Descripcion, @Criticidad, @Detalle, @DVH, @Flag ); SELECT SCOPE_IDENTITY(); ";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);

            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, fechaHora);
                db.AddInParameter(cmd, "@Usuario", DbType.String, usuario);
                db.AddInParameter(cmd, "@Descripcion", DbType.String, accion);
                db.AddInParameter(cmd, "@Criticidad", DbType.String, criticidad);
                db.AddInParameter(cmd, "@Detalle", DbType.String, detalle);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);
                db.AddInParameter(cmd, "@Flag", DbType.Int32, 0);

                // Ejecuto la consulta y devuelve si inserto o no.
                return (Convert.ToBoolean(db.ExecuteScalar(cmd)));

            }

        }

        public void BalancearRegistrosHistoricos()
        {
            const string sqlStatement = "UPDATE dbo.SEG_Bitacora SET[Historico] = 1; "+
                                        "UPDATE dbo.SEG_Bitacora SET[Historico] = 0 WHERE id IN (SELECT TOP 30 Id FROM dbo.SEG_Bitacora ORDER BY Id DESC);";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                
                db.ExecuteNonQuery(cmd);
            }

        }

        private string InvertirFecha(string fecha)
        {

            var anio = fecha.Substring(0, 4);
            var mes = fecha.Substring(5, 2);
            var dia = fecha.Substring(8, 2);

            return (dia + "-" + mes + "-" + anio);

        }

        
        private Bitacora MapearBitacora(IDataReader dr)
        {
            var bitacora = new Bitacora
            {
                Id = GetDataValue<Int64>(dr, "Id"),
                FechaHora = GetDataValue<DateTime>(dr, "FechaHora"),
                Usuario = DescifrarTripleDES(GetDataValue<string>(dr, "Usuario")),
                Accion = DescifrarTripleDES(GetDataValue<string>(dr, "Accion")),
                Criticidad = DescifrarTripleDES(GetDataValue<string>(dr, "Criticidad")),
                Detalle = DescifrarTripleDES(GetDataValue<string>(dr, "Detalle")),
                DVH = GetDataValue<Int64>(dr, "DVH"),

            };
            return bitacora;
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