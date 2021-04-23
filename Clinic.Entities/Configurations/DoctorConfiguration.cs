using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.Configurations
{
    public class DoctorConfiguration : EntityTypeConfiguration<Doctor>
    {
        public DoctorConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(15);
            Property(x => x.Surname).HasMaxLength(20);
            Property(x => x.ConsultingRoom).HasMaxLength(5);
            HasMany(x => x.Visits).WithRequired(x => x.Doctor).HasForeignKey(x => x.DoctorId);
            //NOTE: test
            HasRequired(x => x.Specialization).WithMany(x => x.Doctors).HasForeignKey(x => x.SpecializationId);
        }
    }
}
