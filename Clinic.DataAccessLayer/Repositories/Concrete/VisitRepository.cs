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
    public class VisitRepository : BaseRepository, IVisitRepository
    {

        public async Task<Visit> GetVisitAsync(int id)
        {
            return await context.Visits.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Visit>> GetVisitsAsync()
        {
            return await context.Visits.ToListAsync();
        }

        public async Task<bool> SaveVisitAsync(Visit visit)
        {
            if (visit == null)
                return false;

            try
            {
                context.Entry(visit).State = visit.Id == default(int) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteVisitAsync(Visit visit)
        {
            if (visit == null)
                return false;
            context.Visits.Remove(visit);

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

        public async Task<List<Visit>> GetVisitsAsync(int doctorId, DateTime date)
        {
            var visits = context.Visits.Where(x => (x.DoctorId == doctorId && x.Date.CompareTo(date) == 0));
            return await visits.ToListAsync();
        }
    }
}
