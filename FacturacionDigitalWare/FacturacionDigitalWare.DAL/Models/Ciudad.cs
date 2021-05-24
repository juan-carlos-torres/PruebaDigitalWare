using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            ClienteCliIdCiudadNacimientoNavigations = new HashSet<Cliente>();
            ClienteCliIdCiudadResidenciaNavigations = new HashSet<Cliente>();
        }

        public Guid CiuIdCiudad { get; set; }
        public Guid CiuIdDepartamento { get; set; }
        public string CiuNombre { get; set; }
        public bool CiuActivo { get; set; }

        public virtual Departamento CiuIdDepartamentoNavigation { get; set; }
        public virtual ICollection<Cliente> ClienteCliIdCiudadNacimientoNavigations { get; set; }
        public virtual ICollection<Cliente> ClienteCliIdCiudadResidenciaNavigations { get; set; }
    }
}
