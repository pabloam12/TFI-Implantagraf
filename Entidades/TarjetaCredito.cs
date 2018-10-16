using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class TarjetaCredito
    {
        public string Titular { get; set; }

        public int Numero { get; set; }

        public string Marca { get; set; }

                public int MesVenc { get; set; }

        public int AnioVenc { get; set; }

        public int CodigoV { get; set; }

    }
}