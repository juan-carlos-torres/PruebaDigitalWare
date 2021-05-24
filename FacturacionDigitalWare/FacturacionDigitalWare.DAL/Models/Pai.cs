using System;
using System.Collections.Generic;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class Pai
    {
        public Pai()
        {
            Departamentos = new HashSet<Departamento>();
        }

        public Guid PaiIdPais { get; set; }
        public string PaiNombre { get; set; }
        public bool PaiActivo { get; set; }

        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
