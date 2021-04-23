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
    public class SpecializationRepository : BaseRepository, ISpecializationRepository
    {

        public async Task<Specialization> GetSpecializationAsync(int id)
        {
            return await context.Specializations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Specialization>> GetSpecializationsAsync()
        {
            return await context.Specializations.ToListAsync();
        }

        public async Task<bool> SaveSpecializationAsync(Specialization specialization)
        {
            if (specialization == null)
                return false;
            try
            {
                context.Entry(specialization).State = specialization.Id == default(int) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteSpecializationAsync(Specialization specialization)
        {
            if (specialization == null)
                return false;

            context.Specializations.Remove(specialization);

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
