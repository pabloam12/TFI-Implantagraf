﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioProducto
    {
        public Producto BuscarPorId(int id)
        {
            var ad = new ProductoDAC();

            return ad.BuscarPorId(id);

        }

    }
}