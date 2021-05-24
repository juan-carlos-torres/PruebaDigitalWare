using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class Producto
    {
        public Producto()
        {
            FacturaProductos = new HashSet<FacturaProducto>();
            Inventarios = new HashSet<Inventario>();
        }

        public Guid ProIdProducto { get; set; }
        public string ProNombre { get; set; }
        public string ProDescripcion { get; set; }
        public decimal ProPrecio { get; set; }
        public DateTime ProFechaCreacion { get; set; }
        public bool ProActivo { get; set; }

        public virtual ICollection<FacturaProducto> FacturaProductos { get; set; }
        public virtual ICollection<Inventario> Inventarios { get; set; }
    }
}
