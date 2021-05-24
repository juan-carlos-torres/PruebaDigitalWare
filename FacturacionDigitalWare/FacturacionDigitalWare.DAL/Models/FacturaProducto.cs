using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class FacturaProducto
    {
        public Guid FprIdCompra { get; set; }
        public Guid FprIdProducto { get; set; }
        public int FprCantidad { get; set; }
        public decimal FprValor { get; set; }

        public virtual Factura FprIdCompraNavigation { get; set; }
        public virtual Producto FprIdProductoNavigation { get; set; }
    }
}
