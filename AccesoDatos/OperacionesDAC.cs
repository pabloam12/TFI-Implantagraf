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
            const string sqlStatement = "INSERT INTO dbo.Factura  ([FechaHora], [Tipo], [Monto], [FormaPagoId], [EstadoId], [ClienteId], [DVH])" +
            "VALUES(@FechaHora, @Tipo, @Monto, @FormaPagoId, @EstadoId, @ClienteId, @DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, factura.FechaHora);
                db.AddInParameter(cmd, "@Tipo", DbType.String, factura.Tipo);
                db.AddInParameter(cmd, "@Monto", DbType.Int32, factura.Monto);
                db.AddInParameter(cmd, "@FormaPagoId", DbType.Int32, factura.FormaPago.Id);
                db.AddInParameter(cmd, "@EstadoId", DbType.String, factura.Estado.Id);
                db.AddInParameter(cmd, "@ClienteId", DbType.Int32, factura.Cliente.Id);                
                db.AddInParameter(cmd, "@DVH", DbType.Int64, 0);

                // Ejecuto la consulta y guardo el id que devuelve.
                factura.Codigo = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return factura;

        }


        public Operacion RegistrarOperacion(Operacion operacion)
        {
            const string sqlStatement = "INSERT INTO dbo.Operacion  ([ClienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [EstadoId], [FacturaId], [DVH])" +
            "VALUES(@ClienteId, @FechaHora, @TipoOperacion, @FormaPagoId, @ImporteTotal, @EstadoId, @FacturaId, @DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@ClienteId", DbType.Int32, operacion.Cliente.Id);
                db.AddInParameter(cmd, "@FechaHora", DbType.DateTime, operacion.FechaHora);
                db.AddInParameter(cmd, "@TipoOperacion", DbType.String, operacion.TipoOperacion);
                db.AddInParameter(cmd, "@FormaPagoId", DbType.Int32, operacion.FormaPago.Id);
                db.AddInParameter(cmd, "@ImporteTotal", DbType.Int32, operacion.ImporteTotal);
                db.AddInParameter(cmd, "@EstadoId", DbType.String, operacion.Estado.Id);
                db.AddInParameter(cmd, "@FacturaId", DbType.Int32, operacion.Factura.Codigo);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, 0);

                // Ejecuto la consulta y guardo el id que devuelve.
                operacion.Id = (Convert.ToInt32(db.ExecuteScalar(cmd)));
            }

            return operacion;

        }


        public void RegistrarDetalleOperacion(DetalleOperacion detalleActual)
        {

            const string sqlStatement = "INSERT INTO dbo.DetalleOperacion  ([OperacionId], [ProductoId], [Monto], [Cantidad], [SubTotal], [DVH])" +
            "VALUES(@OperacionId, @ProductoId, @Monto, @Cantidad, @SubTotal, @DVH); SELECT SCOPE_IDENTITY();";

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@OperacionId", DbType.Int32, detalleActual.OperacionId);
                db.AddInParameter(cmd, "@ProductoId", DbType.Int32, detalleActual.ProductoId);
                db.AddInParameter(cmd, "@Monto", DbType.Int32, detalleActual.Monto);
                db.AddInParameter(cmd, "@Cantidad", DbType.Int32, detalleActual.Cantidad);
                db.AddInParameter(cmd, "@SubTotal", DbType.Int32, detalleActual.SubTotal);
                db.AddInParameter(cmd, "@DVH", DbType.Int64, 0);

                db.ExecuteScalar(cmd);
            }

        }


        public List<Operacion> ListarOperaciones()
        {

            const string sqlStatement = "SELECT [Id], [ClienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [EstadoId], [FacturaId], [DVH] " +
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

            string sqlStatement = "SELECT [Id], [ClienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [EstadoId], [FacturaId], [DVH] " +
               "FROM dbo.Operacion WHERE TipoOperacion='" + tipo + "'; ";

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

        public List<Operacion> ListarVentasPorCliente(string clienteId)
        {

            string sqlStatement = "SELECT [Id], [ClienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [EstadoId], [FacturaId], [DVH] " +
               "FROM dbo.Operacion WHERE ClienteId='" + clienteId + "'; ";

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

        public List<Operacion> ListarVentasPorFiltro(string fecha, string fechaFin)
        {

            var sqlStatement = "SELECT [Id], [ClienteId], [FechaHora], [TipoOperacion], [FormaPagoId], [ImporteTotal], [EstadoId], [FacturaId], [DVH] " +
               "FROM dbo.Operacion ";

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

          
            if (whereStatement == "")

            { whereStatement = "Where [TipoOperacion] = 'VE' "; }

            else { whereStatement = whereStatement + "AND [TipoOperacion] = 'VE' "; }

            sqlStatement = sqlStatement + whereStatement + "ORDER BY [FechaHora];";


            var result = new List<Operacion>();
            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@fecha", DbType.String, fecha);
                db.AddInParameter(cmd, "@fechaFin", DbType.String, fechaFin);

                using (var dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        var operacionActual = MapearOperacion(dr); // Mapper
                        result.Add(operacionActual);
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
                db.AddInParameter(cmd, "@OperacionId", DbType.Int32, operacionId);

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

        public List<Factura> ListarFacturas()
        {
            const string sqlStatement = "SELECT [Codigo], [FechaHora], [Tipo], [ClienteId], [Monto], [FormaPagoId], [EstadoId], [DVH]" +
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

        public List<Factura> ListarFacturasporCliente(int clienteId)
        {
            string sqlStatement = "SELECT [Codigo], [FechaHora], [Tipo], [ClienteId], [Monto], [FormaPagoId], [EstadoId], [DVH]" +
               "FROM dbo.Factura WHERE ClienteId=@ClienteId; ";

            var result = new List<Factura>();

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@ClienteId", DbType.Int32, clienteId);

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

        public Factura BuscarFacturaporCodigo(int codFactura)
        {
            string sqlStatement = "SELECT [Codigo], [FechaHora], [Tipo], [ClienteId], [Monto], [FormaPagoId], [EstadoId], [DVH]" +
               "FROM dbo.Factura WHERE Codigo=@CodFactura; ";

            Factura factura = null;

            var db = DatabaseFactory.CreateDatabase(ConnectionName);
            using (var cmd = db.GetSqlStringCommand(sqlStatement))
            {
                db.AddInParameter(cmd, "@CodFactura", DbType.Int32, codFactura);

                using (var dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read()) factura = MapearFactura(dr); // Mapper
                }
            }

            return factura;

        }


        private Operacion MapearOperacion(IDataReader dr)
        {
            var accDatosEstadoOperacion = new EstadoOperacionDAC();
            var accDatosCliente = new ClienteDAC();
            var accDatosFormaPago = new FormaPagoDAC();

            var operacion = new Operacion
            {
                Id = GetDataValue<Int32>(dr, "Id"),
                Cliente = accDatosCliente.BuscarPorId(GetDataValue<Int32>(dr, "ClienteId")), // Mapper Cliente.
                FechaHora = GetDataValue<DateTime>(dr, "FechaHora"),
                TipoOperacion = GetDataValue<string>(dr, "TipoOperacion"),
                FormaPago = accDatosFormaPago.BuscarPorId(GetDataValue<Int32>(dr, "FormaPagoId")), //Mapper FormaPago.
                ImporteTotal = GetDataValue<Int32>(dr, "ImporteTotal"),
                Estado = accDatosEstadoOperacion.BuscarPorId(GetDataValue<Int32>(dr, "EstadoId")),//Mapper EstadoOperacion.
                Factura = BuscarFacturaporCodigo(GetDataValue<Int32>(dr, "FacturaId")),//Mapper Factura.
                DetalleProductos = ListarDetalleporOperacion(GetDataValue<Int32>(dr, "Id")), // Mapper DetalleOperaciones.
                DVH = GetDataValue<Int64>(dr, "DVH")
                
            };
            return operacion;
        }

        private DetalleOperacion MapearDetalleOperacion(IDataReader dr)
        {
            var detalleOperacion = new DetalleOperacion
            {
                OperacionId = GetDataValue<Int32>(dr, "OperacionId"),
                ProductoId = GetDataValue<Int32>(dr, "ProductoId"),
                Monto = GetDataValue<Int32>(dr, "Monto"),
                Cantidad = GetDataValue<Int32>(dr, "Cantidad"),
                SubTotal = GetDataValue<Int32>(dr, "SubTotal"),
                DVH = GetDataValue<Int64>(dr, "DVH")

            };

            return detalleOperacion;
        }

        private Factura MapearFactura(IDataReader dr)
        {
            var datosCliente = new ClienteDAC();
            var datosEstadoOperacion = new EstadoOperacionDAC();
            var accDatosFormaPago = new FormaPagoDAC();

            var factura = new Factura
            {
                Codigo = GetDataValue<Int32>(dr, "Codigo"),
                FechaHora = GetDataValue<DateTime>(dr, "FechaHora"),
                Tipo = GetDataValue<string>(dr, "Tipo"),
                Cliente = datosCliente.BuscarPorId(GetDataValue<Int32>(dr, "ClienteId")), // Mapper Cliente.
                Monto = GetDataValue<Int32>(dr, "Monto"),
                FormaPago = accDatosFormaPago.BuscarPorId(GetDataValue<Int32>(dr, "FormaPagoId")), // Mapper FormaPago.
                Estado = datosEstadoOperacion.BuscarPorId(GetDataValue<Int32>(dr, "EstadoId")), // Mapper EstadoOperacion.
                DVH = GetDataValue<Int64>(dr, "DVH")

            };

            return factura;
        }

        private string InvertirFecha(string fecha)
        {

            var anio = fecha.Substring(0, 4);
            var mes = fecha.Substring(5, 2);
            var dia = fecha.Substring(8, 2);

            return (dia + "-" + mes + "-" + anio);

        }

    }
}