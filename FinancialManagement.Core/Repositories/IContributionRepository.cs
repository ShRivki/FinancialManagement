using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;

namespace FinancialManagement.Core.Repositories
{
    public interface IContributionRepository
    {
        public Task<IEnumerable<Contribution>> GetAsync();

        public Task<Contribution> GetAsync(int id);

        public Task<Contribution> PostAsync(Contribution value);

        public Task<Contribution> PutAsync(int id, Contribution value);

        public Task<Contribution> DeleteAsync(int id);
    }
}
