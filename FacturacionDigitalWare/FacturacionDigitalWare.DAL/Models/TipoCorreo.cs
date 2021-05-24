using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class TipoCorreo
    {
        public TipoCorreo()
        {
            ClienteCorreos = new HashSet<ClienteCorreo>();
        }

        public Guid TcoIdTipoCorreo { get; set; }
        public string TcoDescripcion { get; set; }
        public bool TcoActivo { get; set; }

        public virtual ICollection<ClienteCorreo> ClienteCorreos { get; set; }
    }
}
