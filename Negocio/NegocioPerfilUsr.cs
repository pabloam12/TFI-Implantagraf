﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioPerfilUsr
    {

        public IEnumerable<PerfilUsr> Listar()
        {
            var ad = new PerfilUsrDAC();

            return (ad.Listar());

        }

        public PerfilUsr Agregar(PerfilUsr perfilUsr, string usuario)
        {
            var ad = new PerfilUsrDAC();
            var integ = new IntegridadDatos();
                        var aud = new Auditoria();

            var DVH = integ.CalcularDVH(perfilUsr.Id.ToString() + perfilUsr.Descripcion);

            perfilUsr = ad.Agregar(perfilUsr, DVH);

            integ.RecalcularDVV("SEG_PerfilUsr");

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA PERFIL DE USUARIO", "INFO", "Se creó el perfil de usuario: " + perfilUsr.Id + " - '" + perfilUsr.Descripcion + "'");

            return (perfilUsr);

        }

        public void ActualizarPorId(PerfilUsr perfilUsr, string usuario)
        {
            var ad = new PerfilUsrDAC();
            var integ = new IntegridadDatos();
            var aud = new Auditoria();

            var descripcionAnterior = BuscarPorId(perfilUsr.Id).Descripcion;

            var DVH = integ.CalcularDVH(perfilUsr.Id.ToString() + perfilUsr.Descripcion);

            ad.ActualizarPorId(perfilUsr, DVH);

            integ.RecalcularDVV("SEG_PerfilUsr");

            aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR PERFIL DE USUARIO", "INFO", "Se actualizó el perfil de usuario: " + perfilUsr.Id + " - '" + descripcionAnterior + "' a '" + perfilUsr.Descripcion + "'");

        }

        public void BorrarPorId(PerfilUsr perfilUsr, string usuario)
        {
            var ad = new PerfilUsrDAC();

            ad.BorrarPorId(perfilUsr.Id);

            
            var aud = new Auditoria();

            

            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR PERFIL DE USUARIO", "INFO", "Se borró el perfil de usuario: " + perfilUsr.Id + " - '" + perfilUsr.Descripcion + "'");

        }

        public PerfilUsr BuscarPorId(int id)
        {
            var ad = new PerfilUsrDAC();

            return (ad.BuscarPorId(id));

        }

    }
}