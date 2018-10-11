using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Producto
    {
        public int Codigo { get; set; }

        public string Modelo { get; set; }

        public string Titulo { get; set; }

        public string Imagen { get; set; }

        public string Descripcion { get; set; }

        public Marca Marca { get; set; }

        public Categoria Categoria { get; set; }

        public int Precio { get; set; }

        public long DVH { get; set; }

    }
}