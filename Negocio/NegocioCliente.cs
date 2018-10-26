using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using Seguridad;

namespace Negocio
{
    public class NegocioCliente

    {
        public Cliente TraerCliente(int codUsuario)
        {
            var adCliente = new ClienteDAC();
            var adUsuario = new CuentaDAC();
            var inte = new IntegridadDatos();
            var aud = new Auditoria();

            if (ExisteCliente(codUsuario)!=0)
            {
                return adCliente.BuscarPorId(codUsuario);
            }

            var usuario = adUsuario.ListarUsuarioPorId(codUsuario);

            // Registro al Cliente en base a los datos del Usuario.
            var clienteActual = adCliente.RegistrarCliente(usuario);

            clienteActual.DVH = inte.CalcularDVH(clienteActual.Id.ToString() + clienteActual.RazonSocial + clienteActual.CUIL + clienteActual.Email + clienteActual.Telefono + clienteActual.Direccion + clienteActual.FechaAlta.ToString() + clienteActual.Localidad.Id.ToString());

            // Actualiza el DVH y DVV.
            inte.ActualizarDVHCliente(clienteActual.Id, clienteActual.DVH);
            inte.RecalcularDVV("Cliente");

            // Grabo en Bitácora.                       
            aud.grabarBitacora(DateTime.Now, usuario.Usr, "ALTA CLIENTE", "INFO", "Se registró al Cliente: " + clienteActual.Id.ToString() + " - '" + clienteActual.RazonSocial);


            return clienteActual;

        }

        private int ExisteCliente(int codUsuario)
        {
            var ad = new ClienteDAC();

            return(ad.ExisteCliente(codUsuario));
           
        }

        public List<Cliente> Listar()
        {
            var ad = new ClienteDAC();

            return (ad.Listar());

        }

    }
}