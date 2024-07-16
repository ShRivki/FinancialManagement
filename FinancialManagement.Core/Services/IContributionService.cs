using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Services
{
    public interface IContributionService
    {
        public Task<IEnumerable<Contribution>> GetAllAsync();


        public Task<Contribution> GetContributionByIdAsync(int id);


        public Task<Contribution> PostContributionAsync(Contribution value);


        public Task<Contribution> PutContributionAsync(int id, Contribution value);


        public Task<Contribution> DeleteContributionAsync(int id);
    }
}
