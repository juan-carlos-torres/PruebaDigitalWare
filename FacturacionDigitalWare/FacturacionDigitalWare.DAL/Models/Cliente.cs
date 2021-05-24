using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteCorreos = new HashSet<ClienteCorreo>();
            ClienteTelefonos = new HashSet<ClienteTelefono>();
            Facturas = new HashSet<Factura>();
        }

        public Guid CliIdCliente { get; set; }
        public string CliNombres { get; set; }
        public string CliApellidos { get; set; }
        public Guid CliIdTipoIdentificacion { get; set; }
        public string CliIdentificacion { get; set; }
        public Guid CliIdCiudadNacimiento { get; set; }
        public DateTime CliFechaNacimiento { get; set; }
        public Guid? CliIdCiudadResidencia { get; set; }
        public string CliDireccion { get; set; }
        public DateTime CliFechaCreacion { get; set; }
        public bool CliActivo { get; set; }

        public virtual Ciudad CliIdCiudadNacimientoNavigation { get; set; }
        public virtual Ciudad CliIdCiudadResidenciaNavigation { get; set; }
        public virtual TipoIdentificacion CliIdTipoIdentificacionNavigation { get; set; }
        public virtual ICollection<ClienteCorreo> ClienteCorreos { get; set; }
        public virtual ICollection<ClienteTelefono> ClienteTelefonos { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
