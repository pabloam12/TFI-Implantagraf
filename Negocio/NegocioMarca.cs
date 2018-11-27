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
            

            var marcaActual = ad.Agregar(marca);
            
            var inte = new IntegridadDatos();

            var marcaActualDVH = inte.CalcularDVH(marcaActual.Id.ToString() + marcaActual.Descripcion);
            
            inte.ActualizarDVHMarca(marcaActual.Id, marcaActualDVH);
            inte.RecalcularDVV("Marca");

            var aud = new Auditoria();
            
            aud.grabarBitacora(DateTime.Now, usuario, "ALTA MARCA", "INFO", "Se creó la marca: " + marca.Id + " - '" + marca.Descripcion + "'");

            return (marcaActual);

        }

        public void ActualizarPorId(Marca marca, string usuario)
        {
            var ad = new MarcaDAC();
            
            var aud = new Auditoria();

            var descripcionAnterior = BuscarPorId(marca.Id).Descripcion;

            ad.ActualizarPorId(marca);

            var inte = new IntegridadDatos();

            var marcaActualDVH = inte.CalcularDVH(marca.Id.ToString() + marca.Descripcion);

            inte.ActualizarDVHMarca(marca.Id, marcaActualDVH);
            inte.RecalcularDVV("Marca");

            aud.grabarBitacora(DateTime.Now, usuario, "MODIFICAR MARCA", "INFO", "Se actualizó la marca: " + marca.Id + " - '" + descripcionAnterior + "' a '" + marca.Descripcion + "'");
            
        }

        public void BorrarPorId(Marca marca, string usuario)
        {
            var ad = new MarcaDAC();

            ad.BorrarPorId(marca.Id);

            var aud = new Auditoria();
            
            aud.grabarBitacora(DateTime.Now, usuario, "BORRAR MARCA", "INFO", "Se borró la marca: " + marca.Id + " - '" + marca.Descripcion + "'");

        }

        public Marca BuscarPorId(int id)
        {
            var ad = new MarcaDAC();

            return (ad.BuscarPorId(id));

        }

    }
}