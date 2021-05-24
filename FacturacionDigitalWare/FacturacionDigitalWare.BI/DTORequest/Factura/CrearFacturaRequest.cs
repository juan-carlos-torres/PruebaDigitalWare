using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDigitalWare.BI.DTORequest.Factura
{
    public class CrearFacturaRequest
    {
        public Guid IdCliente { get; set; }

        [MinLength(1, ErrorMessage = "Debe haber al menos {1} producto")]
        public List<Producto> ListadoProductos { get; set; }
        public class Producto
        {
            public Guid Id { get; set; }

            [Min(double.Epsilon, ErrorMessage = "El campo tiene que ser mayor a 0")]
            public int Cantidad { get; set; }

            [Min(double.Epsilon, ErrorMessage = "El campo tiene que ser mayor a 0")]
            public decimal Valor { get; set; }
        }
    }
}
