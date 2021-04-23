using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccessLayer.Repositories.Abstract
{
    public interface IVisitRepository
    {
        Task<Visit> GetVisitAsync(int id);
        Task<List<Visit>> GetVisitsAsync();
        Task<List<Visit>> GetVisitsAsync(int doctorId, DateTime date);
        Task<bool> SaveVisitAsync(Visit visit);
        Task<bool> DeleteVisitAsync(Visit visit);
    }
}
