using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionDigitalWare.BI.DTOResponse.Factura
{
    public class FacturaDetalleResponse
    {
        public string NombreCliente { get; set; }
        public string Fecha { get; set; }
        public decimal ValorTotal { get; set; }

        public List<Producto> ListadoProductos { get; set; }

        public class Producto
        {
            public string Nombre { get; set; }
            public decimal Cantidad { get; set; }
            public decimal Valor { get; set; }
        }
    }
}
