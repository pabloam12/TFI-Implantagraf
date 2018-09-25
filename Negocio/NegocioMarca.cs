using AccesoDatos;
using Entidades;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocio
{
    public class NegocioMarca
    {
        public IEnumerable<Marca> Listar()
        {
            var ad = new MarcaDAC();

            return (ad.Listar());

        }

        public Marca Agregar(Marca marca, string usuario)
        {
            var ad = new MarcaDAC();

            marca = ad.Agregar(marca);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var BitacoraDVH = inte.CalcularDVH(usuario + "ALTA MARCA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "ALTA MARCA", "INFO", "Se creó la marca: " + marca.Id + " - '" + marca.Descripcion + "'", BitacoraDVH);

            return (marca);

        }

        public void ActualizarPorId(Marca marca, string usuario)
        {
            var ad = new MarcaDAC();

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            var descripcionAnterior = BuscarPorId(marca.Id).Descripcion;

            ad.ActualizarPorId(marca);

            var BitacoraDVH = inte.CalcularDVH(usuario + "ACTUALIZAR MARCA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR MARCA", "INFO", "Se actualizó la marca: " + marca.Id + " - '" + descripcionAnterior + "' a '" + marca.Descripcion + "'", BitacoraDVH);


        }

        public void BorrarPorId(Marca marca, string usuario)
        {
            var ad = new MarcaDAC();

            ad.BorrarPorId(marca.Id);

            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            ad.ActualizarPorId(marca);

            var BitacoraDVH = inte.CalcularDVH(usuario + "BORRAR MARCA" + "INFO");

            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR MARCA", "INFO", "Se borró la marca: " + marca.Id + " - '" + marca.Descripcion + "'", BitacoraDVH);

        }

        public Marca BuscarPorId(int id)
        {
            var ad = new MarcaDAC();

            return (ad.BuscarPorId(id));

        }

    }
}