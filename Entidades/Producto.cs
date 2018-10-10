using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public Marca Marca { get; set; }

        public Categoria Categoria { get; set; }

        public int Precio { get; set; }
        
    }
}