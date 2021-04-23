using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [DisplayName("Imię")]
        public string Name { get; set; }
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }
        [DisplayName("Numer gabinetu")]
        public string ConsultingRoom { get; set; }
        [DisplayName("Identyfikator specjalizacji")]
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
