using Entidades;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace AccesoDatos
{
    public class OperacionesDAC : DataAccessComponent

    {
        public Factura RegistrarFactura(Factura factura)
        {
            const string sqlStatement = "INSERT INTO dbo.Factura  ([FechaHora], [Tipo], [Monto], [FormaPagoId], [Estado], [Direccion], [RazonSocial], [Email], [NroTarjeta], [DVH])" +
            "VALUES(@FechaHora, @Tipo, @Monto, @FormaPagoId, @Estado, @Direccion, @RazonSocial, @Email, @NroTarjeta, @DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, factura.FechaHora);
                db.AddInParameter(cmd, "@Tipo", DbType.String, factura.Tipo);
                db.AddInParameter(cmd, "@Monto", DbType.Int64, factura.Monto);
                db.AddInParameter(cmd, "@FormaPagoId", DbType.Int32, factura.FormaPagoId);
                db.AddInParameter(cmd, "@Estado", DbType.String, factura.Estado);
                db.AddInParameter(cmd, "@Direccion", DbType.String, factura.Direccion);
                db.AddInParameter(cmd, "@RazonSocial", DbType.String, factura.RazonSocial);
                db.AddInParameter(cmd, "@Email", DbType.String, factura.Email);
                db.AddInParameter(cmd, "@NroTarjeta", DbType.Int64, factura.NroTarjeta);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, factura.DVH);

                // Ejecuto la consulta y guardo el id que devuelve.
                factura.Codigo = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return factura;

        }


        public Operacion RegistrarOperacion(Operacion operacion)
        {
            const string sqlStatement = "INSERT INTO dbo.Operacion  ([ClienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [Estado], [FacturaId], [DVH])" +
            "VALUES(@ClienteId, @FechaHora, @TipoOperacion, @FormaPagoId, @ImporteTotal, @Estado, @FacturaId, @DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@ClienteId", DbType.Int32, operacion.ClienteId);
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, operacion.FechaHora);
                db.AddInParameter(cmd, "@TipoOperacion", DbType.String, operacion.TipoOperacion);
                db.AddInParameter(cmd, "@FormaPagoId", DbType.Int32, operacion.FormaPagoId);
                db.AddInParameter(cmd, "@ImporteTotal", DbType.Int64, operacion.ImporteTotal);
                db.AddInParameter(cmd, "@Estado", DbType.String, operacion.Estado);
                db.AddInParameter(cmd, "@FacturaId", DbType.Int32, operacion.FacturaId);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, operacion.DVH);

                // Ejecuto la consulta y guardo el id que devuelve.
                operacion.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return operacion;

        }


        public void RegistrarDetalleOperacion(int operacionId, int productoId, double monto, int cantidad, double subtotal, long DVH)
        {

            const string sqlStatement = "INSERT INTO dbo.DetalleOperacion  ([OperacionId], [ProductoId], [Monto], [Cantidad], [SubTotal], [DVH])" +
            "VALUES(@OperacionId, @ProductoId, @Monto, @Cantidad, @SubTotal, @DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@OperacionId", DbType.Int32, operacionId);
                db.AddInParameter(cmd, "@ProductoId", DbType.Int32, productoId);
                db.AddInParameter(cmd, "@Monto", DbType.Int64, monto);
                db.AddInParameter(cmd, "@Cantidad", DbType.Int32, cantidad);
                db.AddInParameter(cmd, "@SubTotal", DbType.Int64, subtotal);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, DVH);

                db.ExecuteScalar(cmd);
            }

        }


        public List<Operacion> ListarOperaciones()
        {

            const string sqlStatement = "SELECT [Id], [CLienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [Estado], [FacturaId] " +
               "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH] " +
               "FROM dbo.Operacion;";

            var result = new List<Operacion>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var operacion = MapearOperacion(dr); // Mapper
                        result.Add(operacion);
                    }
                }
            }

            return result;
        }

        public List<Operacion> ListarOperacionesporTipo(string tipo)
        {

            string sqlStatement = "SELECT [Id], [CLienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [Estado], [FacturaId] " +
               "[Direccion], [LocalidadId], [FechaNacimiento], [FechaAlta], [PerfilId], [IdiomaId], [DVH] " +
               "FROM dbo.Operacion WHERE TipoOperacion=" + tipo + "; ";

            var result = new List<Operacion>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var operacion = MapearOperacion(dr); // Mapper
                        result.Add(operacion);
                    }
                }
            }

            return result;
        }

        public List<DetalleOperacion> ListarDetalleOperacion()
        {
            const string sqlStatement = "SELECT [OperacionId], [ProductoId], [Monto], [Cantidad], [SubTotal], [DVH]" +
               "FROM dbo.DetalleOperacion;";

            var result = new List<DetalleOperacion>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var detalleoperacion = MapearDetalleOperacion(dr); // Mapper
                        result.Add(detalleoperacion);
                    }
                }
            }

            return result;

        }

        public List<DetalleOperacion> ListarDetalleporOperacion(int operacionId)
        {
            const string sqlStatement = "SELECT [OperacionId], [ProductoId], [Monto], [Cantidad], [SubTotal], [DVH]" +
               "FROM dbo.DetalleOperacion " +
               "WHERE OperacionId = @OperacionId;";

            var result = new List<DetalleOperacion>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                using (var dr = db.ExecuteReader(cmd))
                {
                    db.AddInParameter(cmd, "@OperacionId", DbType.Int32, operacionId);

                    while (dr.Read())
                    {
                        var detalleoperacion = MapearDetalleOperacion(dr); // Mapper
                        result.Add(detalleoperacion);
                    }
                }
            }

            return result;

        }

        public List<Factura> ListarFacturas()
        {
            const string sqlStatement = "SELECT [Codigo], [FechaHora], [Tipo], [RazonSocial], [Monto], [FormaPagoId], [NroTarjeta], [Direccion], [Email], [Estado], [DVH]" +
               "FROM dbo.Factura;";

            var result = new List<Factura>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var factura = MapearFactura(dr); // Mapper
                        result.Add(factura);
                    }
                }
            }

            return result;

        }

        public List<Factura> ListarFacturasporCliente(string razonSocial)
        {
            string sqlStatement = "SELECT [Codigo], [FechaHora], [Tipo], [RazonSocial], [Monto], [FormaPagoId], [NroTarjeta], [Direccion], [Email], [Estado], [DVH]" +
               "FROM dbo.Factura WHERE RazonSocial=" + razonSocial + "; ";

            var result = new List<Factura>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var factura = MapearFactura(dr); // Mapper
                        result.Add(factura);
                    }
                }
            }

            return result;

        }


        private Operacion MapearOperacion(IDataReader dr)
        {
            var operacion = new Operacion
            {
                Id = GetDataValue<int>(dr, "Id"),
                ClienteId = GetDataValue<Int32>(dr, "ClienteId"),
                FechaHora = GetDataValue<DateTime>(dr, "FechaHora"),
                TipoOperacion = GetDataValue<string>(dr, "ClienteId")


            };
            return operacion;
        }

        private DetalleOperacion MapearDetalleOperacion(IDataReader dr)
        {
            var detalleOperacion = new DetalleOperacion
            {
                OperacionId = GetDataValue<Int32>(dr, "OperacionId"),
                ProductoId = GetDataValue<Int32>(dr, "ProductoId"),
                Monto = GetDataValue<Int64>(dr, "Monto"),
                Cantidad = GetDataValue<Int32>(dr, "Cantidad"),
                SubTotal = GetDataValue<Int64>(dr, "SubTotal"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };

            return detalleOperacion;
        }

        private Factura MapearFactura(IDataReader dr)
        {
            var factura = new Factura
            {
                Codigo = GetDataValue<Int32>(dr, "Codigo"),
                FechaHora = GetDataValue<DateTime>(dr, "FechaHora"),
                Tipo = GetDataValue<string>(dr, "Tipo"),
                RazonSocial = GetDataValue<string>(dr, "RazonSocial"),
                Monto = GetDataValue<Int64>(dr, "Monto"),
                FormaPagoId = GetDataValue<Int32>(dr, "FormaPagoId"),
                NroTarjeta = GetDataValue<Int64>(dr, "NroTarjeta"),
                Direccion = GetDataValue<string>(dr, "Direccion"),
                Email = GetDataValue<string>(dr, "Email"),
                Estado = GetDataValue<string>(dr, "Estado"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };

            return factura;
        }

    }
}