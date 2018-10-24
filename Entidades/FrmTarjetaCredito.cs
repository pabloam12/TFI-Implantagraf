using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmTarjetaCredito
    {
        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        public string Titular { get; set; }

        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "El Nro de Tarjeta debe ser numérico de 16 dígitos.")]
        [Required(ErrorMessage = "El Nro de Tarjeta es Obligatorio.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "La Empresa es Obligatoria.")]
        public Marca_TC Marca_TC { get; set; }

        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "El Mes debe ser numérico de 2 dígitos.")]
        [Required(ErrorMessage = "El Mes de Vencimiento es Obligatorio.")]
        public string MesVenc { get; set; }

        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "El Año debe ser numérico de 2 dígitos.")]
        [Required(ErrorMessage = "El Año de Vencimiento es Obligatorio.")]
        public string AnioVenc { get; set; }

        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "El Código Verificador debe ser numérico de 3 dígitos.")]
        [Required(ErrorMessage = "El Código Verificador es Obligatorio.")]
        public string CodigoV { get; set; }
        
    }
}