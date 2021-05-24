using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class ClienteTelefono
    {
        public Guid CteIdCliente { get; set; }
        public Guid CteIdTipoTelefono { get; set; }
        public string CteTelefono { get; set; }

        public virtual Cliente CteIdClienteNavigation { get; set; }
        public virtual TipoTelefono CteIdTipoTelefonoNavigation { get; set; }
    }
}
