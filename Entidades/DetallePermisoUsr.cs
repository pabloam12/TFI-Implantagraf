using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class DetallePermisoUsr
    {

        public int Id { get; set; }

        public int UsrId { get; set; }
   
        public int PermisoId { get; set; }
        public string Descripcion { get; set; }

        public string Otorgado { get; set; }
        public long DVH { get; set; }
    }
}