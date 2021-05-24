using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class Inventario
    {
        public Guid InvIdInventario { get; set; }
        public Guid InvIdProducto { get; set; }
        public int InvCantidad { get; set; }

        public virtual Producto InvIdProductoNavigation { get; set; }
    }
}
