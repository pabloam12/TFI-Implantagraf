using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Carrito
    {
               
        public int ProductoId { get; set; }
        
        public string Descripcion { get; set; }
        
        public int Precio { get; set; }

        public int Cantidad { get; set; }
        
        public int CantidadItems { get; set; }
    }
}