using Entidades;
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

        public List<PerfilUsr> Listar()
        {
            var ad = new PerfilUsrDAC();

            return (ad.Listar());

        }

        public PerfilUsr Agregar(PerfilUsr perfilUsr, string usuario)
        {
            var ad = new PerfilUsrDAC();

            perfilUsr = ad.Agregar(perfilUsr);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "ALTA PERFIL DE USUARIO" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA PERFIL DE USUARIO", "INFO", "Se creó el perfil de usuario: " + perfilUsr.Id + " - '" + perfilUsr.Descripcion + "'", BitacoraDVH);

            return (perfilUsr);

        }

        public void ActualizarPorId(PerfilUsr perfilUsr, string usuario)
        {
            var ad = new PerfilUsrDAC();

            var descripcionAnterior = BuscarPorId(perfilUsr.Id).Descripcion;

            ad.ActualizarPorId(perfilUsr);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "ACTUALIZAR PERFIL DE USUARIO" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR PERFIL DE USUARIO", "INFO", "Se actualizó el perfil de usuario: " + perfilUsr.Id + " - '" + descripcionAnterior + "' a '" + perfilUsr.Descripcion + "'", BitacoraDVH);

        }

        public void BorrarPorId(PerfilUsr perfilUsr, string usuario)
        {
            var ad = new PerfilUsrDAC();

            ad.BorrarPorId(perfilUsr.Id);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "BORRAR PERFIL DE USUARIO" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR PERFIL DE USUARIO", "INFO", "Se borró el perfil de usuario: " + perfilUsr.Id + " - '" + perfilUsr.Descripcion + "'", BitacoraDVH);

        }

        public PerfilUsr BuscarPorId(int id)
        {
            var ad = new PerfilUsrDAC();

            return (ad.BuscarPorId(id));

        }

    }
}