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
        public Producto BuscarPorId(int id)
        {
            const string sqlStatement = "SELECT [Id], [Nombre], [MarcaId], [CategoriaId], [Precio] " +
                "FROM dbo.Producto WHERE [ID]=@Id ";

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
                Id = GetDataValue<int>(dr, "Id"),
                Nombre = GetDataValue<string>(dr, "Nombre"),
                Marca = marcaDAC.BuscarPorId(GetDataValue<int>(dr, "MarcaId")), //Mapper
                Categoria = categoriaDAC.BuscarPorId(GetDataValue<int>(dr, "CategoriaId")), //Mapper
                Precio = GetDataValue<int>(dr, "Precio")

            };
            return producto;
        }
    }
}