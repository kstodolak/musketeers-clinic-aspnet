using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccessLayer.Repositories.Abstract
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientAsync(string account_id);
        Task<List<Patient>> GetPatientsAsync();
        Task<bool> SavePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(Patient patient);
    }
}
