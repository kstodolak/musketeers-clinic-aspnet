using Clinic.DataAccessLayer.Repositories.Abstract;
using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccessLayer.Repositories.Concrete
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        public async Task<Patient> GetPatientAsync(string account_id)
        {
            return await context.Patients.FirstOrDefaultAsync(x => x.AccountId == account_id);
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await context.Patients.ToListAsync();
        }

        public async Task<bool> SavePatientAsync(Patient patient)
        {
            if (patient == null)
                return false;
            try
            {
                context.Entry(patient).State = patient.Id == default(int) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeletePatientAsync(Patient patient)
        {
            if (patient == null)
                return false;
            context.Patients.Remove(patient);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
