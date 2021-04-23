using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.Configurations
{
    public class SpecializationConfiguration :EntityTypeConfiguration<Specialization>
    {
        public SpecializationConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(25);
            HasMany(x => x.Doctors).WithRequired(x => x.Specialization).HasForeignKey(x => x.SpecializationId);
        }
    }
}
