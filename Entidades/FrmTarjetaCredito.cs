using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmTarjetaCredito
    {
        //Español
        [Required(ErrorMessage = "El {0} es Obligatorio.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "El Titular solo puede contener letras.")]
        [MaxLength(50, ErrorMessage = "El Titular no puede superar los {1} caractéres.")]
        public string Titular { get; set; }

        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "El Nro de Tarjeta debe ser numérico de 16 dígitos.")]
        [Required(ErrorMessage = "El Nro de Tarjeta es Obligatorio.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "La Empresa es Obligatoria.")]
        public Marca_TC Marca_TC { get; set; }

        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "El Mes debe ser numérico de 2 dígitos.")]
        [Required(ErrorMessage = "El Mes de Vencimiento es Obligatorio.")]
        [Range(1, 12, ErrorMessage = "El valor debe estar entre 1 y 12.")]
        public string MesVenc { get; set; }

        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "El Año debe ser numérico de 2 dígitos.")]
        [Required(ErrorMessage = "El Año de Vencimiento es Obligatorio.")]
        [Range(1, 99, ErrorMessage = "El valor debe estar entre 1 y 99.")]
        public string AnioVenc { get; set; }

        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "El Código Verificador debe ser numérico de 3 dígitos.")]
        [Required(ErrorMessage = "El Código Verificador es Obligatorio.")]
        [DataType(DataType.Password)]
        public string CodigoV { get; set; }


        //Inglés

        [Required(ErrorMessage = "This field is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "This field must be letters.")]
        [MaxLength(50, ErrorMessage = "This field can not be longer than 50 characters.")]
        public string Titular_Eng { get; set; }

        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Credit Number must be numeric and 16 digits.")]
        [Required(ErrorMessage = "This field is Required.")]
        public string Numero_Eng { get; set; }

        [Required(ErrorMessage = "This field is Required.")]
        public Marca_TC Marca_TC_Eng { get; set; }

        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "Expiration Month must be numeric and 2 digits.")]
        [Required(ErrorMessage = "This field is Required.")]
        [Range(1, 12, ErrorMessage = "This field must be between 1 and 12.")]
        public string MesVenc_Eng { get; set; }

        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "Expiration Year must be numeric and 2 digits.")]
        [Required(ErrorMessage = "This field is Required.")]
        [Range(1, 99, ErrorMessage = "This field must be between 1 and 99.")]
        public string AnioVenc_Eng { get; set; }

        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "The secret Code must be numeric and 3 digits.")]
        [Required(ErrorMessage = "This field is Required.")]
        [DataType(DataType.Password)]
        public string CodigoV_Eng { get; set; }

    }
}