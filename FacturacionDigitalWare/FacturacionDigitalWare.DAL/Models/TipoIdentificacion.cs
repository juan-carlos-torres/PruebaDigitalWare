using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class TipoIdentificacion
    {
        public TipoIdentificacion()
        {
            Clientes = new HashSet<Cliente>();
        }

        public Guid TidIdTipoIdentificacion { get; set; }
        public string TidDescripcion { get; set; }
        public bool TidActivo { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
