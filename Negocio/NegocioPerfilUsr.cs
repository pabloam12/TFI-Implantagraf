using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;

namespace Negocio
{
    public class NegocioPerfilUsr
    {
        public PerfilUsr Agregar(PerfilUsr perfilUsr)
        {
            var ad = new PerfilUsrDAC();

            return (ad.Agregar(perfilUsr));

        }

        public void ActualizarPorId(PerfilUsr perfilUsr)
        {
            var ad = new PerfilUsrDAC();

            ad.ActualizarPorId(perfilUsr);

        }

        public void BorrarPorId(int id)
        {
            var ad = new PerfilUsrDAC();

            ad.BorrarPorId(id);

        }

        public void ListarPorId(int id)
        {
            var ad = new PerfilUsrDAC();

            ad.ListarPorId(id);

        }

        public List<PerfilUsr> Listar()
        {
            var ad = new PerfilUsrDAC();

            return (ad.Listar());

        }


    }
}