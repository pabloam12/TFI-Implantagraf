using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class FrmOlvidoPsw
    {

        [Required(ErrorMessage = "El Correo de Usuario es Obligatorio")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "El {0} no es válido.")]
        public string Usuario { get; set; }

    }
}