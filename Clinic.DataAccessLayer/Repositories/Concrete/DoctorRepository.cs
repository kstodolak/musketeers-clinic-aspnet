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
    public class DoctorRepository : BaseRepository, IDoctorRepository
    {
        public async Task<Doctor> GetDoctorAsync(int id)
        {
            return await context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Doctor>> GetDoctorsAsync()
        {
            return await context.Doctors.ToListAsync();
        }

        public async Task<List<Doctor>> GetDoctorsBySpecializationAsync(int specializationId)
        {
            var specialization = await context.Specializations.FirstOrDefaultAsync(x => x.Id == specializationId);
            if (specialization == null)
                return new List<Doctor>();

            return specialization.Doctors.ToList();
        }
        public async Task<bool> SaveDoctorAsync(Doctor doctor)
        {
            if (doctor == null)
                return false;

            try
            {

                if (doctor.SpecializationId == default(int))
                    throw new Exception("No specialization id given");

                var specialization = await context.Specializations.FirstOrDefaultAsync(x => x.Id == doctor.SpecializationId);
                if (specialization == null)
                    throw new Exception("No specialization with given id");
                doctor.Specialization = specialization;

                context.Entry(doctor).State = doctor.Id == default(int) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
                
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteDoctorAsync(Doctor doctor)
        {
            if (doctor == null)
                return false;
            context.Doctors.Remove(doctor);

            try
            {
                await context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
