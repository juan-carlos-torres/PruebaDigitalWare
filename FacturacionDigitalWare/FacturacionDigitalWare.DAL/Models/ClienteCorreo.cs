using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class ClienteCorreo
    {
        public Guid CcoIdCliente { get; set; }
        public Guid CcoIdTipoCorreo { get; set; }
        public string CcoCorreo { get; set; }
        public bool CcoActivo { get; set; }

        public virtual Cliente CcoIdClienteNavigation { get; set; }
        public virtual TipoCorreo CcoIdTipoCorreoNavigation { get; set; }
    }
}
