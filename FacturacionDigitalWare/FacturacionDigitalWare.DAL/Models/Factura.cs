using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class Factura
    {
        public Factura()
        {
            FacturaProductos = new HashSet<FacturaProducto>();
        }

        public Guid FacIdFactura { get; set; }
        public Guid FacIdCliente { get; set; }
        public DateTime FacFechaRegistro { get; set; }
        public decimal FacValorTotal { get; set; }

        public virtual Cliente FacIdClienteNavigation { get; set; }
        public virtual ICollection<FacturaProducto> FacturaProductos { get; set; }
    }
}
