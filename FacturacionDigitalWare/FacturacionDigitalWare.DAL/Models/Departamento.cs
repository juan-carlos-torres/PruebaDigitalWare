using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Ciudads = new HashSet<Ciudad>();
        }

        public Guid DepIdDepartamento { get; set; }
        public Guid DepIdPais { get; set; }
        public string DepNombre { get; set; }
        public bool DepActivo { get; set; }

        public virtual Pai DepIdPaisNavigation { get; set; }
        public virtual ICollection<Ciudad> Ciudads { get; set; }
    }
}
