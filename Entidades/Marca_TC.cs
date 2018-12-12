using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Marca_TC
    {
        [Required(ErrorMessage = "Complete")]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        
    }
}