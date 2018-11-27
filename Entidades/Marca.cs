using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Marca
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La Descripción es Obligatoria")]
        [MaxLength(30, ErrorMessage = "La Descripción no puede superar los {1} caracteres.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "La Descripción solo puede contener letras.")]
        public string Descripcion { get; set; }

        public long DVH { get; set; }
    }
}
