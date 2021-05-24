using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionDigitalWare.BI.DTORequest.Producto
{
    public class CrearEditarProductoRequest
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(200, ErrorMessage = "Máximo {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(500, ErrorMessage = "Máximo {1} caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Min(double.Epsilon, ErrorMessage = "El campo tiene que ser mayor a 0")]
        public decimal Precio { get; set; }

        public bool Activo { get; set; }
    }
}
