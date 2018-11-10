using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class ProductoDAC : DataAccessComponent

    {
        public List<Producto> ListarProductos()
        {
            const string sqlStatement = "SELECT [Codigo], [Modelo], [Titulo], [Titulo_Eng], [Imagen], [Descripcion], [Descripcion_Eng], [MarcaId], [CategoriaId], [Precio], [DVH] " +
               "FROM dbo.Producto;";

            var result = new List<Producto>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var producto = MapearProducto(dr); // Mapper
                        result.Add(producto);
                    }
                }
            }

            return result;

        }

        public Producto BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Codigo], [Modelo], [Titulo], [Titulo_Eng], [Imagen], [Descripcion], [Descripcion_Eng], [MarcaId], [CategoriaId], [Precio], [DVH] " +
                "FROM dbo.Producto WHERE [Codigo]=@Id ";

            Producto producto = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) producto = MapearProducto(dr); // Mapper
                }
            }

            return producto;
        }


        private static Producto MapearProducto(IDataReader dr)
        {
            var categoriaDAC = new CategoriaDAC();
            var marcaDAC = new MarcaDAC();

            var producto = new Producto
            {
                Codigo = GetDataValue<int>(dr, "Codigo"),
                Modelo = GetDataValue<string>(dr, "Modelo"),
                Titulo = GetDataValue<string>(dr, "Titulo"),
                Titulo_Eng = GetDataValue<string>(dr, "Titulo_Eng"),
                Imagen = GetDataValue<string>(dr, "Imagen"),
                Descripcion = GetDataValue<string>(dr, "Descripcion"),
                Descripcion_Eng = GetDataValue<string>(dr, "Descripcion_Eng"),
                Marca = marcaDAC.BuscarPorId(GetDataValue<int>(dr, "MarcaId")), //Mapper
                Categoria = categoriaDAC.BuscarPorId(GetDataValue<int>(dr, "CategoriaId")), //Mapper
                Precio = GetDataValue<int>(dr, "Precio"),
                DVH = GetDataValue<Int64>(dr, "DVH")
            };
            return producto;
        }
    }
}