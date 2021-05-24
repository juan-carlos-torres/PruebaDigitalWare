using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionDigitalWare.BI.DTOResponse.Producto
{
    public class ProductoDetalleResponse
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string FechaCreacion { get; set; }

        public bool Activo { get; set; }
    }
}
