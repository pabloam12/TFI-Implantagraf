using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Presentacion.Models
{
    public class CuentaDBContext : DbContext
    {
        public DbSet<CuentaUsuario> cuentaUsuario { get; set; }
    }
}