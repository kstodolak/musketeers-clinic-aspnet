using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.Models
{
    public class Specialization
    {
        [DisplayName("Identyfikator specjalizacji")]

        public int Id { get; set; }
        [DisplayName("Nazwa specjalizacji")]

        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
