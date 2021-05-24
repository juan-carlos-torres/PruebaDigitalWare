using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class TipoTelefono
    {
        public TipoTelefono()
        {
            ClienteTelefonos = new HashSet<ClienteTelefono>();
        }

        public Guid TteIdTipoTelefono { get; set; }
        public string TteDescripcion { get; set; }
        public bool TteActivo { get; set; }

        public virtual ICollection<ClienteTelefono> ClienteTelefonos { get; set; }
    }
}
