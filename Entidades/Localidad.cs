using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Localidad
    {
        [Required(ErrorMessage = "La Localidad es Obligatoria.")]
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public long DVH { get; set; }
    }
}