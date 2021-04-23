using Clinic.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccessLayer.Repositories.Abstract
{
    public interface ISpecializationRepository
    {
        Task<Specialization> GetSpecializationAsync(int id);
        Task<List<Specialization>> GetSpecializationsAsync();
        Task<bool> SaveSpecializationAsync(Specialization specialization);
        Task<bool> DeleteSpecializationAsync(Specialization specialization);
    }
}
