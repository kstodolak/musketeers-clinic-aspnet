using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccessLayer.Repositories.Abstract
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetDoctorAsync(int id);
        Task<List<Doctor>> GetDoctorsAsync();
        Task<List<Doctor>> GetDoctorsBySpecializationAsync(int specializationId);
        Task<bool> SaveDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(Doctor doctor);
    }
}
