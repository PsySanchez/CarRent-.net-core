using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class ManufacturersCar
    {
        public ManufacturersCar()
        {
            ModelsCar = new HashSet<ModelsCar>();
        }

        public int Id { get; set; }
        public string ManufacturerName { get; set; }

        public virtual ICollection<ModelsCar> ModelsCar { get; set; }
    }
}
