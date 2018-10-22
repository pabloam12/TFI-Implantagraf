﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FormaPago
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public long DVH { get; set; }
    }
}