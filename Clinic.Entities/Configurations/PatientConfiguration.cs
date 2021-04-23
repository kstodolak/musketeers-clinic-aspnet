using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.Configurations
{
    class PatientConfiguration : EntityTypeConfiguration<Patient>
    {
        public PatientConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(20);
            Property(x => x.Surname).HasMaxLength(20);
            Property(x => x.Adress).HasMaxLength(50);
            Property(x => x.PostCode).HasMaxLength(6);
            Property(x => x.City).HasMaxLength(25);
            Property(x => x.AccountId).HasMaxLength(128);
            HasMany(x => x.Visits).WithRequired(x => x.Patient).HasForeignKey(x => x.PatientId);
        }
    }
}
