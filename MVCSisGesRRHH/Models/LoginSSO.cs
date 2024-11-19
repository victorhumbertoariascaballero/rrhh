using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSisGesRRHH.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class LoginSSO
    {
        public int IdUsuario { get; set; }
        [Required]
        [Display(Name="Usuario")]
        public string NombreUsuario { get; set; }
        [Required, DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }
        [DataType(DataType.Password)]
        public string ClaveNuevaTexto { get; set; }
        [DataType(DataType.Password)]
        public string ClaveNuevaConfirmaTexto { get; set; }

        public byte[] ClaveNueva { get; set; }
        
    }
}